using HslCommunication;
using HslCommunication.Core;
using HslCommunication.Core.Net;
using HslCommunication.LogNet;
using Newtonsoft.Json.Linq;
using SharpNodeSettings.Node.Device;
using SharpNodeSettings.Node.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using SharpNodeSettings.Node.Regular;
using System.Xml.Linq;

namespace SharpNodeSettings.Device
{
    /// <summary>
    /// 设备交互的核心类对象
    /// </summary>
    public class DeviceCore
    {

        #region Constructor

        /// <summary>
        /// 使用默认的无参构造方法
        /// </summary>
        public DeviceCore( )
        {
            ActiveTime = DateTime.Now.AddDays( -1 );
            autoResetQuit = new AutoResetEvent( false );
            JObjectData = new JObject( );
            jsonTmp = JObjectData.ToString( );
            jsonLock = new SimpleHybirdLock( );
        }

        #endregion

        #region IDeviceCore Properties
        
        /// <summary>
        /// 设备分布的信息点
        /// </summary>
        public string[] DeviceNodes { get; set; }
        
        /// <summary>
        /// 所有的请求列表
        /// </summary>
        public List<DeviceRequest> Requests { get; set; }

        /// <summary>
        /// 数据转换规则
        /// </summary>
        public IByteTransform ByteTransform { get; set; }

        /// <summary>
        /// 当前的数据读写信息
        /// </summary>
        public IReadWriteNet ReadWriteDevice { get; set; }

        /// <summary>
        /// 指示读取到数据后应该如何处理
        /// </summary>
        public Action<string[], string, dynamic> WriteCustomerData { get; set; }
        

        /// <summary>
        /// 设备上次激活的时间节点，用来判断失效状态
        /// </summary>
        public DateTime ActiveTime { get; set; }

        /// <summary>
        /// 唯一的识别码，方便异形客户端寻找对应的处理逻辑
        /// </summary>
        public string UniqueId { get; set; }

        /// <summary>
        /// 设备的名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 请求成功的次数统计
        /// </summary>
        public long RequestSuccessCount { get; set; }

        /// <summary>
        /// 请求失败的次数统计
        /// </summary>
        public long RequestFailedCount { get; set; }

        /// <summary>
        /// 类型名称
        /// </summary>
        public string TypeName { get; set; }


        
        /// <summary>
        /// 指示设备是否正常的状态
        /// </summary>
        public bool IsError { get; set; }

        /// <summary>
        /// 获取本设备所有的属性数据
        /// </summary>
        public string JsonData { get => jsonTmp; }
        
        #endregion

        #region Protect Method

        /// <summary>
        /// 使用固定的节点加载数据信息
        /// </summary>
        /// <param name="element">数据请求的所有列表信息</param>
        protected void LoadRequest( XElement element )
        {
            Requests = new List<DeviceRequest>( );
            foreach (var item in element.Elements( "DeviceRequest" ))
            {
                DeviceRequest request = new DeviceRequest( );
                request.LoadByXmlElement( item );
                Requests.Add( request );
            }
        }

        #endregion

        #region Public Method

        /// <summary>
        /// 启动读取数据
        /// </summary>
        public void StartRead( )
        {
            if (Interlocked.CompareExchange( ref isStarted, 1, 0 ) == 0)
            {
                thread = new Thread( new ThreadStart( ThreadReadBackground ) );
                thread.IsBackground = true;
                thread.Priority = ThreadPriority.AboveNormal;
                thread.Start( );
            }
        }

        /// <summary>
        /// 退出系统
        /// </summary>
        public void QuitDevice( )
        {
            if (isStarted == 1)
            {
                isQuit = 1;
                autoResetQuit.WaitOne( );
            }
        }

        /// <summary>
        /// 设置为异形客户端对象
        /// </summary>
        /// <param name="alienSession">异形对象</param>
        public virtual void SetAlineSession( AlienSession alienSession )
        {

        }

        /// <summary>
        /// 通过节点值名称，获取本设备信息的值
        /// </summary>
        /// <param name="name">节点值名称</param>
        /// <returns>动态值</returns>
        public dynamic GetValueByName( string name )
        {
            dynamic result = "[Null]";
            jsonLock.Enter( );

            if(JObjectData.ContainsKey( name ))
            {
                result = JObjectData[name];
            }

            jsonLock.Leave( );
            return result;
        }

        /// <summary>
        /// 判断当前的设备是否是传入的节点参数信息
        /// </summary>
        /// <param name="nodes">传入的节点参数信息</param>
        /// <returns>是否是当前的设备</returns>
        public bool IsCurrentDevice( string[] nodes )
        {
            if (DeviceNodes != null && nodes != null)
            {
                for (int i = 0; i < DeviceNodes.Length; i++)
                {
                    if(DeviceNodes[i] != nodes[i])
                    {
                        return false;
                    }
                }

                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 获取本设备对象的值信息
        /// </summary>
        /// <param name="nodes">节点数据</param>
        /// <returns>值信息数据</returns>
        public string GetValueByName( string[] nodes )
        {
            if (nodes.Length == DeviceNodes.Length)
            {
                return jsonTmp;
            }
            else if(nodes.Length > DeviceNodes.Length)
            {
                return GetValueByName( nodes[DeviceNodes.Length] );
            }
            else
            {
                return string.Empty;
            }
        }

        #endregion

        #region Virtual Method

        /// <summary>
        /// 在启动之前进行的操作信息
        /// </summary>
        protected virtual void BeforStart( )
        {

        }

        /// <summary>
        /// 在关闭的时候需要进行的操作
        /// </summary>
        protected virtual void AfterClose( )
        {

        }

        #endregion

        #region Thread Read
        
        private void ThreadReadBackground( )
        {
            Thread.Sleep( 1000 );           // 默认休息一下下
            BeforStart( );                  // 需要子类重写

            while (isQuit == 0)
            {
                Thread.Sleep( 100 );

                bool isDataChange = false;           // 数据是否发生了变化
                foreach (var Request in Requests)
                {
                    if ((DateTime.Now - Request.LastActiveTime).TotalMilliseconds > Request.CaptureInterval)
                    {
                        Request.LastActiveTime = DateTime.Now;

                        OperateResult<byte[]> read = ReadWriteDevice.Read( Request.Address, Request.Length );
                        if (read.IsSuccess)
                        {
                            IsError = false;
                            isDataChange = true;
                            ParseFromRequest( read.Content, Request );
                            ActiveTime = DateTime.Now;
                            RequestSuccessCount++;
                        }
                        else
                        {
                            IsError = true;
                            RequestFailedCount++;
                        }
                    }
                }

                // 更新Json字符串缓存
                jsonLock.Enter( );
                if (isDataChange) jsonTmp = JObjectData.ToString( );
                jsonLock.Leave( );
            }


            AfterClose( );                            // 需要子类重写
            autoResetQuit.Set( );                     // 通知关闭的线程继续
        }

        #endregion

        #region Private Member

        private Thread thread;                   // 后台读取的线程
        private int isStarted = 0;               // 是否启动了后台数据读取
        private AutoResetEvent autoResetQuit;    // 退出系统的时候的同步锁
        private int isQuit = 0;                  // 是否准备从系统进行退出
        private ILogNet logNet;                  // 系统的日志
        private readonly JObject JObjectData;    // JSON数据中心
        private string jsonTmp = string.Empty;   // JSON数据缓存
        private SimpleHybirdLock jsonLock;       // JSON对象的安全锁

        #endregion

        #region JSON Object

        private void ParseFromRequest( byte[] data, DeviceRequest request )
        {
            jsonLock.Enter( );
            try
            {
                foreach (var regular in request.RegularNodes)
                {
                    dynamic value = regular.GetValue( data, ByteTransform );
                    if(regular.RegularCode != RegularNodeTypeItem.StringAscii.Code &&
                        regular.RegularCode != RegularNodeTypeItem.StringUnicode.Code &&
                        regular.RegularCode != RegularNodeTypeItem.StringUtf8.Code &&
                        regular.TypeLength>1)
                    {
                        // 数组
                        JObjectData[regular.Name] = new JArray(regular.GetValue( data, ByteTransform ));
                    }
                    else
                    {
                        // 单个的值
                        JObjectData[regular.Name] = new JValue(regular.GetValue( data, ByteTransform ));
                    }
                    WriteCustomerData?.Invoke( DeviceNodes, regular.Name, value );
                }
                jsonLock.Leave( );
            }
            catch
            {
                jsonLock.Leave( );
                throw;
            }
        }

        #endregion
        

    }
}
