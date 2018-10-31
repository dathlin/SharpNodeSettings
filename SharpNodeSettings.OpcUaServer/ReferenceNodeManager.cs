/* ========================================================================
 * Copyright (c) 2005-2016 The OPC Foundation, Inc. All rights reserved.
 *
 * OPC Foundation MIT License 1.00
 * 
 * Permission is hereby granted, free of charge, to any person
 * obtaining a copy of this software and associated documentation
 * files (the "Software"), to deal in the Software without
 * restriction, including without limitation the rights to use,
 * copy, modify, merge, publish, distribute, sublicense, and/or sell
 * copies of the Software, and to permit persons to whom the
 * Software is furnished to do so, subject to the following
 * conditions:
 * 
 * The above copyright notice and this permission notice shall be
 * included in all copies or substantial portions of the Software.
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
 * EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES
 * OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND
 * NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT
 * HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY,
 * WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING
 * FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR
 * OTHER DEALINGS IN THE SOFTWARE.
 *
 * The complete license agreement can be found here:
 * http://opcfoundation.org/License/MIT/1.00/
 * ======================================================================*/

using System;
using System.Collections.Generic;
using System.Xml;
using System.Threading;
using Opc.Ua;
using Opc.Ua.Server;
using SharpNodeSettings.Core;
using System.Xml.Linq;
using SharpNodeSettings.Node.Regular;
using SharpNodeSettings.Node.Request;
using SharpNodeSettings.Node.Server;

namespace SharpNodeSettings.OpcUaServer
{
    /// <summary>
    /// A node manager for a server that exposes several variables.
    /// </summary>
    public class EmptyNodeManager : CustomNodeManager2
    {
        #region Constructors
        /// <summary>
        /// Initializes the node manager.
        /// </summary>
        public EmptyNodeManager(IServerInternal server, ApplicationConfiguration configuration)
        :
            base(server, configuration, Namespaces.ReferenceApplications)
        {
            SystemContext.NodeIdFactory = this;

            // get the configuration for the node manager.
            m_configuration = configuration.ParseExtension<ReferenceServerConfiguration>();

            // use suitable defaults if no configuration exists.
            if (m_configuration == null)
            {
                m_configuration = new ReferenceServerConfiguration();
            }

            m_dynamicNodes = new List<BaseDataVariableState>();
        }
        #endregion

        #region IDisposable Members
        /// <summary>
        /// An overrideable version of the Dispose.
        /// </summary>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                // TBD
            }
        }
        #endregion

        #region INodeIdFactory Members
        /// <summary>
        /// Creates the NodeId for the specified node.
        /// </summary>
        public override NodeId New(ISystemContext context, NodeState node)
        {
            BaseInstanceState instance = node as BaseInstanceState;

            if (instance != null && instance.Parent != null)
            {
                string id = instance.Parent.NodeId.Identifier as string;

                if (id != null)
                {
                    return new NodeId(id + "_" + instance.SymbolicName, instance.Parent.NodeId.NamespaceIndex);
                }
            }

            return node.NodeId;
        }
        #endregion

        #region Private Helper Functions
        private static bool IsUnsignedAnalogType(BuiltInType builtInType)
        {
            if (builtInType == BuiltInType.Byte ||
                builtInType == BuiltInType.UInt16 ||
                builtInType == BuiltInType.UInt32 ||
                builtInType == BuiltInType.UInt64)
            {
                return true;
            }
            return false;
        }

        private static bool IsAnalogType(BuiltInType builtInType)
        {
            switch (builtInType)
            {
                case BuiltInType.Byte:
                case BuiltInType.UInt16:
                case BuiltInType.UInt32:
                case BuiltInType.UInt64:
                case BuiltInType.SByte:
                case BuiltInType.Int16:
                case BuiltInType.Int32:
                case BuiltInType.Int64:
                case BuiltInType.Float:
                case BuiltInType.Double:
                    return true;
            }
            return false;
        }

        private static Opc.Ua.Range GetAnalogRange(BuiltInType builtInType)
        {
            switch (builtInType)
            {
                case BuiltInType.UInt16:
                    return new Range(System.UInt16.MaxValue, System.UInt16.MinValue);
                case BuiltInType.UInt32:
                    return new Range(System.UInt32.MaxValue, System.UInt32.MinValue);
                case BuiltInType.UInt64:
                    return new Range(System.UInt64.MaxValue, System.UInt64.MinValue);
                case BuiltInType.SByte:
                    return new Range(System.SByte.MaxValue, System.SByte.MinValue);
                case BuiltInType.Int16:
                    return new Range(System.Int16.MaxValue, System.Int16.MinValue);
                case BuiltInType.Int32:
                    return new Range(System.Int32.MaxValue, System.Int32.MinValue);
                case BuiltInType.Int64:
                    return new Range(System.Int64.MaxValue, System.Int64.MinValue);
                case BuiltInType.Float:
                    return new Range(System.Single.MaxValue, System.Single.MinValue);
                case BuiltInType.Double:
                    return new Range(System.Double.MaxValue, System.Double.MinValue);
                case BuiltInType.Byte:
                    return new Range(System.Byte.MaxValue, System.Byte.MinValue);
                default:
                    return new Range(System.SByte.MaxValue, System.SByte.MinValue);
            }
        }
        #endregion

        #region INodeManager Members
        /// <summary>
        /// Does any initialization required before the address space can be used.
        /// </summary>
        /// <remarks>
        /// The externalReferences is an out parameter that allows the node manager to link to nodes
        /// in other node managers. For example, the 'Objects' node is managed by the CoreNodeManager and
        /// should have a reference to the root folder node(s) exposed by this node manager.  
        /// </remarks>
        public override void CreateAddressSpace( IDictionary<NodeId, IList<IReference>> externalReferences )
        {
            lock (Lock)
            {
                LoadPredefinedNodes( SystemContext, externalReferences );

                IList<IReference> references = null;

                if (!externalReferences.TryGetValue( ObjectIds.ObjectsFolder, out references ))
                {
                    externalReferences[ObjectIds.ObjectsFolder] = references = new List<IReference>( );
                }


                dict_BaseDataVariableState = new Dictionary<string, BaseDataVariableState>( );
                try
                {
                    // =========================================================================================
                    // 
                    // 此处需要加载本地文件，并且创建对应的节点信息，
                    // 
                    // =========================================================================================
                    sharpNodeServer = new SharpNodeServer( );
                    sharpNodeServer.WriteCustomerData = ( Device.DeviceCore deviceCore, string name ) =>
                    {
                        string opcNode = "ns=2;s=" + string.Join( "/", deviceCore.DeviceNodes ) + "/" + name;
                        lock (Lock)
                        {
                            if (dict_BaseDataVariableState.ContainsKey( opcNode ))
                            {
                                dict_BaseDataVariableState[opcNode].Value = deviceCore.GetDynamicValueByName( name );
                                dict_BaseDataVariableState[opcNode].ClearChangeMasks( SystemContext, false );
                            }
                        }
                    };

                    XElement element = XElement.Load( "settings.xml" );
                    dicRegularItemNode = SharpNodeSettings.Util.ParesRegular( element );

                    AddNodeClass( null, element, references );


                    // 加载配置文件之前设置写入方法

                    sharpNodeServer.LoadByXmlFile( "settings.xml" );
                    // 最后再启动服务器信息
                    sharpNodeServer.ServerStart( 12345 );
                }
                catch (Exception e)
                {
                    Utils.Trace( e, "Error creating the address space." );
                }
            }
        }

        private void AddNodeClass( NodeState parent, XElement nodeClass, IList<IReference> references )
        {
            foreach (var xmlNode in nodeClass.Elements( ))
            {
                if (xmlNode.Name == "NodeClass")
                {
                    SharpNodeSettings.Node.NodeBase.NodeClass nClass = new SharpNodeSettings.Node.NodeBase.NodeClass( );
                    nClass.LoadByXmlElement( xmlNode );

                    FolderState son;
                    if (parent == null)
                    {
                        son = CreateFolder( null, nClass.Name );
                        son.Description = nClass.Description;
                        son.AddReference( ReferenceTypes.Organizes, true, ObjectIds.ObjectsFolder );
                        references.Add( new NodeStateReference( ReferenceTypes.Organizes, false, son.NodeId ) );
                        son.EventNotifier = EventNotifiers.SubscribeToEvents;
                        AddRootNotifier( son );
                        
                        AddNodeClass( son, xmlNode, references );
                        AddPredefinedNode( SystemContext, son );
                    }
                    else
                    {
                        son = CreateFolder( parent, nClass.Name, nClass.Description );
                        AddNodeClass( son, xmlNode, references );
                    }
                }
                else if (xmlNode.Name == "DeviceNode")
                {
                    AddDeviceCore( parent, xmlNode );
                }
                else if (xmlNode.Name == "Server")
                {
                    AddServer( parent, xmlNode, references );
                }
            }
        }

        private void AddDeviceCore( NodeState parent, XElement device )
        {
            if (device.Name == "DeviceNode")
            {
                // 提取名称和描述信息
                string name = device.Attribute( "Name" ).Value;
                string description = device.Attribute( "Description" ).Value;

                // 创建OPC节点
                FolderState deviceFolder = CreateFolder( parent, device.Attribute( "Name" ).Value, device.Attribute( "Description" ).Value );
                // 添加Request
                foreach (var requestXml in device.Elements( "DeviceRequest" ))
                {
                    DeviceRequest deviceRequest = new DeviceRequest( );
                    deviceRequest.LoadByXmlElement( requestXml );
                    
                    AddDeviceRequest( deviceFolder, deviceRequest );
                }
            }
        }

        private void AddServer( NodeState parent, XElement xmlNode, IList<IReference> references )
        {
            int serverType = int.Parse( xmlNode.Attribute( "ServerType" ).Value );
            if (serverType == ServerNode.ModbusServer)
            {
                NodeModbusServer serverNode = new NodeModbusServer( );
                serverNode.LoadByXmlElement( xmlNode );

                FolderState son = CreateFolder( parent, serverNode.Name, serverNode.Description );
                AddNodeClass( son, xmlNode, references );
            }
            else if (serverType == ServerNode.AlienServer)
            {
                AlienServerNode alienNode = new AlienServerNode( );
                alienNode.LoadByXmlElement( xmlNode );

                FolderState son = CreateFolder( parent, alienNode.Name, alienNode.Description );
                AddNodeClass( son, xmlNode, references );
            }
        }
        private void AddDeviceRequest( NodeState parent, DeviceRequest deviceRequest )
        {
            // 提炼真正的数据节点
            if (!dicRegularItemNode.ContainsKey( deviceRequest.PraseRegularCode )) return;
            List<RegularItemNode> regularNodes = dicRegularItemNode[deviceRequest.PraseRegularCode];

            foreach (var regularNode in regularNodes)
            {
                if (regularNode.RegularCode == RegularNodeTypeItem.Bool.Code)
                {
                    if (regularNode.TypeLength == 1)
                    {
                        var dataVariableState = CreateBaseVariable( parent, regularNode.Name, regularNode.Description, DataTypeIds.Boolean, ValueRanks.Scalar, default( bool ) );
                        dict_BaseDataVariableState.Add( dataVariableState.NodeId.ToString( ), dataVariableState );
                    }
                    else
                    {
                        var dataVariableState = CreateBaseVariable( parent, regularNode.Name, regularNode.Description, DataTypeIds.Boolean, ValueRanks.OneDimension, new bool[regularNode.TypeLength] );
                        dict_BaseDataVariableState.Add( dataVariableState.NodeId.ToString( ), dataVariableState );
                    }
                }
                else if (regularNode.RegularCode == RegularNodeTypeItem.Byte.Code)
                {
                    if (regularNode.TypeLength == 1)
                    {
                        var dataVariableState = CreateBaseVariable( parent, regularNode.Name, regularNode.Description, DataTypeIds.Byte, ValueRanks.Scalar, default( byte ) );
                        dict_BaseDataVariableState.Add( dataVariableState.NodeId.ToString( ), dataVariableState );
                    }
                    else
                    {
                        var dataVariableState = CreateBaseVariable( parent, regularNode.Name, regularNode.Description, DataTypeIds.Byte, ValueRanks.OneDimension, new byte[regularNode.TypeLength] );
                        dict_BaseDataVariableState.Add( dataVariableState.NodeId.ToString( ), dataVariableState );
                    }
                }
                else if (regularNode.RegularCode == RegularNodeTypeItem.Int16.Code)
                {
                    if (regularNode.TypeLength == 1)
                    {
                        var dataVariableState = CreateBaseVariable( parent, regularNode.Name, regularNode.Description, DataTypeIds.Int16, ValueRanks.Scalar, default( short ) );
                        dict_BaseDataVariableState.Add( dataVariableState.NodeId.ToString( ), dataVariableState );
                    }
                    else
                    {
                        var dataVariableState = CreateBaseVariable( parent, regularNode.Name, regularNode.Description, DataTypeIds.Int16, ValueRanks.OneDimension, new short[regularNode.TypeLength] );
                        dict_BaseDataVariableState.Add( dataVariableState.NodeId.ToString( ), dataVariableState );
                    }
                }
                else if (regularNode.RegularCode == RegularNodeTypeItem.UInt16.Code)
                {
                    if (regularNode.TypeLength == 1)
                    {
                        var dataVariableState = CreateBaseVariable( parent, regularNode.Name, regularNode.Description, DataTypeIds.UInt16, ValueRanks.Scalar, default( ushort ) );
                        dict_BaseDataVariableState.Add( dataVariableState.NodeId.ToString( ), dataVariableState );
                    }
                    else
                    {
                        var dataVariableState = CreateBaseVariable( parent, regularNode.Name, regularNode.Description, DataTypeIds.UInt16, ValueRanks.OneDimension, new ushort[regularNode.TypeLength] );
                        dict_BaseDataVariableState.Add( dataVariableState.NodeId.ToString( ), dataVariableState );
                    }
                }
                else if (regularNode.RegularCode == RegularNodeTypeItem.Int32.Code)
                {
                    if (regularNode.TypeLength == 1)
                    {
                        var dataVariableState = CreateBaseVariable( parent, regularNode.Name, regularNode.Description, DataTypeIds.Int32, ValueRanks.Scalar, default( int ) );
                        dict_BaseDataVariableState.Add( dataVariableState.NodeId.ToString( ), dataVariableState );
                    }
                    else
                    {
                        var dataVariableState = CreateBaseVariable( parent, regularNode.Name, regularNode.Description, DataTypeIds.Int32, ValueRanks.OneDimension, new int[regularNode.TypeLength] );
                        dict_BaseDataVariableState.Add( dataVariableState.NodeId.ToString( ), dataVariableState );
                    }
                }
                else if (regularNode.RegularCode == RegularNodeTypeItem.UInt32.Code)
                {
                    if (regularNode.TypeLength == 1)
                    {
                        var dataVariableState = CreateBaseVariable( parent, regularNode.Name, regularNode.Description, DataTypeIds.UInt32, ValueRanks.Scalar, default( uint ) );
                        dict_BaseDataVariableState.Add( dataVariableState.NodeId.ToString( ), dataVariableState );
                    }
                    else
                    {
                        var dataVariableState = CreateBaseVariable( parent, regularNode.Name, regularNode.Description, DataTypeIds.UInt32, ValueRanks.OneDimension, new uint[regularNode.TypeLength] );
                        dict_BaseDataVariableState.Add( dataVariableState.NodeId.ToString( ), dataVariableState );
                    }
                }
                else if (regularNode.RegularCode == RegularNodeTypeItem.Float.Code)
                {
                    if (regularNode.TypeLength == 1)
                    {
                        var dataVariableState = CreateBaseVariable( parent, regularNode.Name, regularNode.Description, DataTypeIds.Float, ValueRanks.Scalar, default( float ) );
                        dict_BaseDataVariableState.Add( dataVariableState.NodeId.ToString( ), dataVariableState );
                    }
                    else
                    {
                        var dataVariableState = CreateBaseVariable( parent, regularNode.Name, regularNode.Description, DataTypeIds.Float, ValueRanks.OneDimension, new float[regularNode.TypeLength] );
                        dict_BaseDataVariableState.Add( dataVariableState.NodeId.ToString( ), dataVariableState );
                    }
                }
                else if (regularNode.RegularCode == RegularNodeTypeItem.Int64.Code)
                {
                    if (regularNode.TypeLength == 1)
                    {
                        var dataVariableState = CreateBaseVariable( parent, regularNode.Name, regularNode.Description, DataTypeIds.Int64, ValueRanks.Scalar, default( long ) );
                        dict_BaseDataVariableState.Add( dataVariableState.NodeId.ToString( ), dataVariableState );
                    }
                    else
                    {
                        var dataVariableState = CreateBaseVariable( parent, regularNode.Name, regularNode.Description, DataTypeIds.Int64, ValueRanks.OneDimension, new long[regularNode.TypeLength] );
                        dict_BaseDataVariableState.Add( dataVariableState.NodeId.ToString( ), dataVariableState );
                    }
                }
                else if (regularNode.RegularCode == RegularNodeTypeItem.UInt64.Code)
                {
                    if (regularNode.TypeLength == 1)
                    {
                        var dataVariableState = CreateBaseVariable( parent, regularNode.Name, regularNode.Description, DataTypeIds.UInt64, ValueRanks.Scalar, default( ulong ) );
                        dict_BaseDataVariableState.Add( dataVariableState.NodeId.ToString( ), dataVariableState );
                    }
                    else
                    {
                        var dataVariableState = CreateBaseVariable( parent, regularNode.Name, regularNode.Description, DataTypeIds.UInt64, ValueRanks.OneDimension, new ulong[regularNode.TypeLength] );
                        dict_BaseDataVariableState.Add( dataVariableState.NodeId.ToString( ), dataVariableState );
                    }
                }
                else if (regularNode.RegularCode == RegularNodeTypeItem.Double.Code)
                {
                    if (regularNode.TypeLength == 1)
                    {
                        var dataVariableState = CreateBaseVariable( parent, regularNode.Name, regularNode.Description, DataTypeIds.Double, ValueRanks.Scalar, default( double ) );
                        dict_BaseDataVariableState.Add( dataVariableState.NodeId.ToString( ), dataVariableState );
                    }
                    else
                    {
                        var dataVariableState = CreateBaseVariable( parent, regularNode.Name, regularNode.Description, DataTypeIds.Double, ValueRanks.OneDimension, new double[regularNode.TypeLength] );
                        dict_BaseDataVariableState.Add( dataVariableState.NodeId.ToString( ), dataVariableState );
                    }
                }
                else if (regularNode.RegularCode == RegularNodeTypeItem.StringAscii.Code ||
                    regularNode.RegularCode == RegularNodeTypeItem.StringUnicode.Code ||
                    regularNode.RegularCode == RegularNodeTypeItem.StringUtf8.Code)
                {

                    var dataVariableState = CreateBaseVariable( parent, regularNode.Name, regularNode.Description, DataTypeIds.String, ValueRanks.Scalar, "" );
                    dict_BaseDataVariableState.Add( dataVariableState.NodeId.ToString( ), dataVariableState );
                }
            }

        }


        /// <summary>
        /// 创建一个新的节点，节点名称为字符串
        /// </summary>
        protected FolderState CreateFolder( NodeState parent, string name )
        {
            return CreateFolder( parent, name, string.Empty );
        }
        
        /// <summary>
        /// 创建一个新的节点，节点名称为字符串
        /// </summary>
        protected FolderState CreateFolder( NodeState parent, string name, string description )
        {
            FolderState folder = new FolderState( parent );

            folder.SymbolicName = name;
            folder.ReferenceTypeId = ReferenceTypes.Organizes;
            folder.TypeDefinitionId = ObjectTypeIds.FolderType;
            folder.Description = description;
            if (parent == null)
            {
                folder.NodeId = new NodeId( name, NamespaceIndex );
            }
            else
            {
                folder.NodeId = new NodeId( parent.NodeId.ToString( ) + "/" + name );
            }
            folder.BrowseName = new QualifiedName( name, NamespaceIndex );
            folder.DisplayName = new LocalizedText( name );
            folder.WriteMask = AttributeWriteMask.None;
            folder.UserWriteMask = AttributeWriteMask.None;
            folder.EventNotifier = EventNotifiers.None;

            if (parent != null)
            {
                parent.AddChild( folder );
            }

            return folder;
        }

        /// <summary>
        /// 创建一个值节点，类型需要在创建的时候指定
        /// </summary>
        protected BaseDataVariableState CreateBaseVariable( NodeState parent, string name, string description, NodeId dataType, int valueRank, object defaultValue )
        {
            BaseDataVariableState variable = new BaseDataVariableState( parent );

            variable.SymbolicName = name;
            variable.ReferenceTypeId = ReferenceTypes.Organizes;
            variable.TypeDefinitionId = VariableTypeIds.BaseDataVariableType;
            if (parent == null)
            {
                variable.NodeId = new NodeId( name, NamespaceIndex );
            }
            else
            {
                variable.NodeId = new NodeId( parent.NodeId.ToString( ) + "/" + name );
            }
            variable.Description = description;
            variable.BrowseName = new QualifiedName( name, NamespaceIndex );
            variable.DisplayName = new LocalizedText( name );
            variable.WriteMask = AttributeWriteMask.DisplayName | AttributeWriteMask.Description;
            variable.UserWriteMask = AttributeWriteMask.DisplayName | AttributeWriteMask.Description;
            variable.DataType = dataType;
            variable.ValueRank = valueRank;
            variable.AccessLevel = AccessLevels.CurrentReadOrWrite;
            variable.UserAccessLevel = AccessLevels.CurrentReadOrWrite;
            variable.Historizing = false;
            variable.Value = defaultValue;
            variable.StatusCode = StatusCodes.Good;
            variable.Timestamp = DateTime.Now;
            if (valueRank == ValueRanks.OneDimension)
            {
                variable.ArrayDimensions = new ReadOnlyList<uint>( new List<uint> { 0 } );
            }
            else if (valueRank == ValueRanks.TwoDimensions)
            {
                variable.ArrayDimensions = new ReadOnlyList<uint>( new List<uint> { 0, 0 } );
            }

            if (parent != null)
            {
                parent.AddChild( variable );
            }

            return variable;
        }

        /// <summary>
        /// Creates a new variable.
        /// </summary>
        private DataItemState CreateDataItemVariable(NodeState parent, string path, string name, BuiltInType dataType, int valueRank)
        {
            DataItemState variable = new DataItemState(parent);
            variable.ValuePrecision = new PropertyState<double>(variable);
            variable.Definition = new PropertyState<string>(variable);

            variable.Create(
                SystemContext,
                null,
                variable.BrowseName,
                null,
                true);

            variable.SymbolicName = name;
            variable.ReferenceTypeId = ReferenceTypes.Organizes;
            variable.NodeId = new NodeId(path, NamespaceIndex);
            variable.BrowseName = new QualifiedName(path, NamespaceIndex);
            variable.DisplayName = new LocalizedText("en", name);
            variable.WriteMask = AttributeWriteMask.None;
            variable.UserWriteMask = AttributeWriteMask.None;
            variable.DataType = (uint)dataType;
            variable.ValueRank = valueRank;
            variable.AccessLevel = AccessLevels.CurrentReadOrWrite;
            variable.UserAccessLevel = AccessLevels.CurrentReadOrWrite;
            variable.Historizing = false;
            variable.Value = Opc.Ua.TypeInfo.GetDefaultValue((uint)dataType, valueRank, Server.TypeTree);
            variable.StatusCode = StatusCodes.Good;
            variable.Timestamp = DateTime.UtcNow;

            if (valueRank == ValueRanks.OneDimension)
            {
                variable.ArrayDimensions = new ReadOnlyList<uint>(new List<uint> { 0 });
            }
            else if (valueRank == ValueRanks.TwoDimensions)
            {
                variable.ArrayDimensions = new ReadOnlyList<uint>(new List<uint> { 0, 0 });
            }

            variable.ValuePrecision.Value = 2;
            variable.ValuePrecision.AccessLevel = AccessLevels.CurrentReadOrWrite;
            variable.ValuePrecision.UserAccessLevel = AccessLevels.CurrentReadOrWrite;
            variable.Definition.Value = String.Empty;
            variable.Definition.AccessLevel = AccessLevels.CurrentReadOrWrite;
            variable.Definition.UserAccessLevel = AccessLevels.CurrentReadOrWrite;

            if (parent != null)
            {
                parent.AddChild(variable);
            }

            return variable;
        }

        private DataItemState[] CreateDataItemVariables(NodeState parent, string path, string name, BuiltInType dataType, int valueRank, UInt16 numVariables)
        {
            List<DataItemState> itemsCreated = new List<DataItemState>();
            // create the default name first:
            itemsCreated.Add(CreateDataItemVariable(parent, path, name, dataType, valueRank));
            // now to create the remaining NUMBERED items
            for (uint i = 0; i < numVariables; i++)
            {
                string newName = string.Format("{0}{1}", name, i.ToString("000"));
                string newPath = string.Format("{0}/Mass/{1}", path, newName);
                itemsCreated.Add(CreateDataItemVariable(parent, newPath, newName, dataType, valueRank));
            }//for i
            return (itemsCreated.ToArray());
        }

        private ServiceResult OnWriteDataItem(
            ISystemContext context,
            NodeState node,
            NumericRange indexRange,
            QualifiedName dataEncoding,
            ref object value,
            ref StatusCode statusCode,
            ref DateTime timestamp)
        {
            DataItemState variable = node as DataItemState;

            // verify data type.
            Opc.Ua.TypeInfo typeInfo = Opc.Ua.TypeInfo.IsInstanceOfDataType(
                value,
                variable.DataType,
                variable.ValueRank,
                context.NamespaceUris,
                context.TypeTable);

            if (typeInfo == null || typeInfo == Opc.Ua.TypeInfo.Unknown)
            {
                return StatusCodes.BadTypeMismatch;
            }

            if (typeInfo.BuiltInType != BuiltInType.DateTime)
            {
                double number = Convert.ToDouble(value);
                number = Math.Round(number, (int)variable.ValuePrecision.Value);
                value = Opc.Ua.TypeInfo.Cast(number, typeInfo.BuiltInType);
            }

            return ServiceResult.Good;
        }

        /// <summary>
        /// Creates a new variable.
        /// </summary>
        private AnalogItemState CreateAnalogItemVariable(NodeState parent, string path, string name, BuiltInType dataType, int valueRank)
        {
            return (CreateAnalogItemVariable(parent, path, name, dataType, valueRank, null));
        }

        private AnalogItemState CreateAnalogItemVariable(NodeState parent, string path, string name, BuiltInType dataType, int valueRank, object initialValues)
        {
            return (CreateAnalogItemVariable(parent, path, name, dataType, valueRank, initialValues, null));
        }

        private AnalogItemState CreateAnalogItemVariable(NodeState parent, string path, string name, BuiltInType dataType, int valueRank, object initialValues, Range customRange)
        {
            return CreateAnalogItemVariable(parent, path, name, (uint)dataType, valueRank, initialValues, customRange);
        }

        private AnalogItemState CreateAnalogItemVariable(NodeState parent, string path, string name, NodeId dataType, int valueRank, object initialValues, Range customRange)
        {
            AnalogItemState variable = new AnalogItemState(parent);
            variable.BrowseName = new QualifiedName(path, NamespaceIndex);
            variable.EngineeringUnits = new PropertyState<EUInformation>(variable);
            variable.InstrumentRange = new PropertyState<Range>(variable);

            variable.Create(
                SystemContext,
                new NodeId(path, NamespaceIndex),
                variable.BrowseName,
                null,
                true);

            variable.NodeId = new NodeId(path, NamespaceIndex);
            variable.SymbolicName = name;
            variable.DisplayName = new LocalizedText("en", name);
            variable.WriteMask = AttributeWriteMask.None;
            variable.UserWriteMask = AttributeWriteMask.None;
            variable.ReferenceTypeId = ReferenceTypes.Organizes;
            variable.DataType = dataType;
            variable.ValueRank = valueRank;
            variable.AccessLevel = AccessLevels.CurrentReadOrWrite;
            variable.UserAccessLevel = AccessLevels.CurrentReadOrWrite;
            variable.Historizing = false;

            if (valueRank == ValueRanks.OneDimension)
            {
                variable.ArrayDimensions = new ReadOnlyList<uint>(new List<uint> { 0 });
            }
            else if (valueRank == ValueRanks.TwoDimensions)
            {
                variable.ArrayDimensions = new ReadOnlyList<uint>(new List<uint> { 0, 0 });
            }

            BuiltInType builtInType = Opc.Ua.TypeInfo.GetBuiltInType(dataType, Server.TypeTree);

            // Simulate a mV Voltmeter
            Range newRange = GetAnalogRange(builtInType);
            // Using anything but 120,-10 fails a few tests
            newRange.High = Math.Min(newRange.High, 120);
            newRange.Low = Math.Max(newRange.Low, -10);
            variable.InstrumentRange.Value = newRange;

            if (customRange != null)
            {
                variable.EURange.Value = customRange;
            }
            else
            {
                variable.EURange.Value = new Range(100, 0);
            }

            if (initialValues == null)
            {
                variable.Value = Opc.Ua.TypeInfo.GetDefaultValue(dataType, valueRank, Server.TypeTree);
            }
            else
            {
                variable.Value = initialValues;
            }

            variable.StatusCode = StatusCodes.Good;
            variable.Timestamp = DateTime.UtcNow;
            // The latest UNECE version (Rev 11, published in 2015) is available here:
            // http://www.opcfoundation.org/UA/EngineeringUnits/UNECE/rec20_latest_08052015.zip
            variable.EngineeringUnits.Value = new EUInformation("mV", "millivolt", "http://www.opcfoundation.org/UA/units/un/cefact");
            // The mapping of the UNECE codes to OPC UA(EUInformation.unitId) is available here:
            // http://www.opcfoundation.org/UA/EngineeringUnits/UNECE/UNECE_to_OPCUA.csv
            variable.EngineeringUnits.Value.UnitId = 12890; // "2Z"
            variable.OnWriteValue = OnWriteAnalog;
            variable.EURange.OnWriteValue = OnWriteAnalogRange;
            variable.EURange.AccessLevel = AccessLevels.CurrentReadOrWrite;
            variable.EURange.UserAccessLevel = AccessLevels.CurrentReadOrWrite;
            variable.EngineeringUnits.AccessLevel = AccessLevels.CurrentReadOrWrite;
            variable.EngineeringUnits.UserAccessLevel = AccessLevels.CurrentReadOrWrite;
            variable.InstrumentRange.OnWriteValue = OnWriteAnalogRange;
            variable.InstrumentRange.AccessLevel = AccessLevels.CurrentReadOrWrite;
            variable.InstrumentRange.UserAccessLevel = AccessLevels.CurrentReadOrWrite;

            if (parent != null)
            {
                parent.AddChild(variable);
            }

            return variable;
        }

        /// <summary>
        /// Creates a new variable.
        /// </summary>
        private DataItemState CreateTwoStateDiscreteItemVariable(NodeState parent, string path, string name, string trueState, string falseState)
        {
            TwoStateDiscreteState variable = new TwoStateDiscreteState(parent);

            variable.NodeId = new NodeId(path, NamespaceIndex);
            variable.BrowseName = new QualifiedName(path, NamespaceIndex);
            variable.DisplayName = new LocalizedText("en", name);
            variable.WriteMask = AttributeWriteMask.None;
            variable.UserWriteMask = AttributeWriteMask.None;

            variable.Create(
                SystemContext,
                null,
                variable.BrowseName,
                null,
                true);

            variable.SymbolicName = name;
            variable.ReferenceTypeId = ReferenceTypes.Organizes;
            variable.DataType = DataTypeIds.Boolean;
            variable.ValueRank = ValueRanks.Scalar;
            variable.AccessLevel = AccessLevels.CurrentReadOrWrite;
            variable.UserAccessLevel = AccessLevels.CurrentReadOrWrite;
            variable.Historizing = false;
            variable.Value = (bool)GetNewValue(variable);
            variable.StatusCode = StatusCodes.Good;
            variable.Timestamp = DateTime.UtcNow;

            variable.TrueState.Value = trueState;
            variable.TrueState.AccessLevel = AccessLevels.CurrentReadOrWrite;
            variable.TrueState.UserAccessLevel = AccessLevels.CurrentReadOrWrite;

            variable.FalseState.Value = falseState;
            variable.FalseState.AccessLevel = AccessLevels.CurrentReadOrWrite;
            variable.FalseState.UserAccessLevel = AccessLevels.CurrentReadOrWrite;

            if (parent != null)
            {
                parent.AddChild(variable);
            }

            return variable;
        }

        /// <summary>
        /// Creates a new variable.
        /// </summary>
        private DataItemState CreateMultiStateDiscreteItemVariable(NodeState parent, string path, string name, params string[] values)
        {
            MultiStateDiscreteState variable = new MultiStateDiscreteState(parent);

            variable.NodeId = new NodeId(path, NamespaceIndex);
            variable.BrowseName = new QualifiedName(path, NamespaceIndex);
            variable.DisplayName = new LocalizedText("en", name);
            variable.WriteMask = AttributeWriteMask.None;
            variable.UserWriteMask = AttributeWriteMask.None;

            variable.Create(
                SystemContext,
                null,
                variable.BrowseName,
                null,
                true);

            variable.SymbolicName = name;
            variable.ReferenceTypeId = ReferenceTypes.Organizes;
            variable.DataType = DataTypeIds.UInt32;
            variable.ValueRank = ValueRanks.Scalar;
            variable.AccessLevel = AccessLevels.CurrentReadOrWrite;
            variable.UserAccessLevel = AccessLevels.CurrentReadOrWrite;
            variable.Historizing = false;
            variable.Value = (uint)0;
            variable.StatusCode = StatusCodes.Good;
            variable.Timestamp = DateTime.UtcNow;
            variable.OnWriteValue = OnWriteDiscrete;

            LocalizedText[] strings = new LocalizedText[values.Length];

            for (int ii = 0; ii < strings.Length; ii++)
            {
                strings[ii] = values[ii];
            }

            variable.EnumStrings.Value = strings;
            variable.EnumStrings.AccessLevel = AccessLevels.CurrentReadOrWrite;
            variable.EnumStrings.UserAccessLevel = AccessLevels.CurrentReadOrWrite;

            if (parent != null)
            {
                parent.AddChild(variable);
            }

            return variable;
        }

        /// <summary>
        /// Creates a new UInt32 variable.
        /// </summary>
        private DataItemState CreateMultiStateValueDiscreteItemVariable(NodeState parent, string path, string name, params string[] enumNames)
        {
            return CreateMultiStateValueDiscreteItemVariable(parent, path, name, null, enumNames);
        }

        /// <summary>
        /// Creates a new variable.
        /// </summary>
        private DataItemState CreateMultiStateValueDiscreteItemVariable(NodeState parent, string path, string name, NodeId nodeId, params string[] enumNames)
        {
            MultiStateValueDiscreteState variable = new MultiStateValueDiscreteState(parent);

            variable.NodeId = new NodeId(path, NamespaceIndex);
            variable.BrowseName = new QualifiedName(path, NamespaceIndex);
            variable.DisplayName = new LocalizedText("en", name);
            variable.WriteMask = AttributeWriteMask.None;
            variable.UserWriteMask = AttributeWriteMask.None;

            variable.Create(
                SystemContext,
                null,
                variable.BrowseName,
                null,
                true);

            variable.SymbolicName = name;
            variable.ReferenceTypeId = ReferenceTypes.Organizes;
            variable.DataType = (nodeId == null) ? DataTypeIds.UInt32 : nodeId;
            variable.ValueRank = ValueRanks.Scalar;
            variable.AccessLevel = AccessLevels.CurrentReadOrWrite;
            variable.UserAccessLevel = AccessLevels.CurrentReadOrWrite;
            variable.Historizing = false;
            variable.Value = (uint)0;
            variable.StatusCode = StatusCodes.Good;
            variable.Timestamp = DateTime.UtcNow;
            variable.OnWriteValue = OnWriteValueDiscrete;

            // there are two enumerations for this type:
            // EnumStrings = the string representations for enumerated values
            // ValueAsText = the actual enumerated value

            // set the enumerated strings
            LocalizedText[] strings = new LocalizedText[enumNames.Length];
            for (int ii = 0; ii < strings.Length; ii++)
            {
                strings[ii] = enumNames[ii];
            }

            // set the enumerated values
            EnumValueType[] values = new EnumValueType[enumNames.Length];
            for (int ii = 0; ii < values.Length; ii++)
            {
                values[ii] = new EnumValueType();
                values[ii].Value = ii;
                values[ii].Description = strings[ii];
                values[ii].DisplayName = strings[ii];
            }
            variable.EnumValues.Value = values;
            variable.EnumValues.AccessLevel = AccessLevels.CurrentReadOrWrite;
            variable.EnumValues.UserAccessLevel = AccessLevels.CurrentReadOrWrite;
            variable.ValueAsText.Value = variable.EnumValues.Value[0].DisplayName;

            if (parent != null)
            {
                parent.AddChild(variable);
            }

            return variable;
        }

        private ServiceResult OnWriteDiscrete(
            ISystemContext context,
            NodeState node,
            NumericRange indexRange,
            QualifiedName dataEncoding,
            ref object value,
            ref StatusCode statusCode,
            ref DateTime timestamp)
        {
            MultiStateDiscreteState variable = node as MultiStateDiscreteState;

            // verify data type.
            Opc.Ua.TypeInfo typeInfo = Opc.Ua.TypeInfo.IsInstanceOfDataType(
                value,
                variable.DataType,
                variable.ValueRank,
                context.NamespaceUris,
                context.TypeTable);

            if (typeInfo == null || typeInfo == Opc.Ua.TypeInfo.Unknown)
            {
                return StatusCodes.BadTypeMismatch;
            }

            if (indexRange != NumericRange.Empty)
            {
                return StatusCodes.BadIndexRangeInvalid;
            }

            double number = Convert.ToDouble(value);

            if (number >= variable.EnumStrings.Value.Length | number < 0)
            {
                return StatusCodes.BadOutOfRange;
            }

            return ServiceResult.Good;
        }

        private ServiceResult OnWriteValueDiscrete(
            ISystemContext context,
            NodeState node,
            NumericRange indexRange,
            QualifiedName dataEncoding,
            ref object value,
            ref StatusCode statusCode,
            ref DateTime timestamp)
        {
            MultiStateValueDiscreteState variable = node as MultiStateValueDiscreteState;

            TypeInfo typeInfo = TypeInfo.Construct(value);

            if (variable == null ||
                typeInfo == null ||
                typeInfo == Opc.Ua.TypeInfo.Unknown ||
                !TypeInfo.IsNumericType(typeInfo.BuiltInType))
            {
                return StatusCodes.BadTypeMismatch;
            }

            if (indexRange != NumericRange.Empty)
            {
                return StatusCodes.BadIndexRangeInvalid;
            }

            Int32 number = Convert.ToInt32(value);
            if (number >= variable.EnumValues.Value.Length || number < 0)
            {
                return StatusCodes.BadOutOfRange;
            }

            if (!node.SetChildValue(context, BrowseNames.ValueAsText, variable.EnumValues.Value[number].DisplayName, true))
            {
                return StatusCodes.BadOutOfRange;
            }

            node.ClearChangeMasks(context, true);

            return ServiceResult.Good;
        }

        private ServiceResult OnWriteAnalog(
            ISystemContext context,
            NodeState node,
            NumericRange indexRange,
            QualifiedName dataEncoding,
            ref object value,
            ref StatusCode statusCode,
            ref DateTime timestamp)
        {
            AnalogItemState variable = node as AnalogItemState;

            // verify data type.
            Opc.Ua.TypeInfo typeInfo = Opc.Ua.TypeInfo.IsInstanceOfDataType(
                value,
                variable.DataType,
                variable.ValueRank,
                context.NamespaceUris,
                context.TypeTable);

            if (typeInfo == null || typeInfo == Opc.Ua.TypeInfo.Unknown)
            {
                return StatusCodes.BadTypeMismatch;
            }

            // check index range.
            if (variable.ValueRank >= 0)
            {
                if (indexRange != NumericRange.Empty)
                {
                    object target = variable.Value;
                    ServiceResult result = indexRange.UpdateRange(ref target, value);

                    if (ServiceResult.IsBad(result))
                    {
                        return result;
                    }

                    value = target;
                }
            }

            // check instrument range.
            else
            {
                if (indexRange != NumericRange.Empty)
                {
                    return StatusCodes.BadIndexRangeInvalid;
                }

                double number = Convert.ToDouble(value);

                if (variable.InstrumentRange != null && (number < variable.InstrumentRange.Value.Low || number > variable.InstrumentRange.Value.High))
                {
                    return StatusCodes.BadOutOfRange;
                }
            }

            return ServiceResult.Good;
        }

        private ServiceResult OnWriteAnalogRange(
            ISystemContext context,
            NodeState node,
            NumericRange indexRange,
            QualifiedName dataEncoding,
            ref object value,
            ref StatusCode statusCode,
            ref DateTime timestamp)
        {
            PropertyState<Range> variable = node as PropertyState<Range>;
            ExtensionObject extensionObject = value as ExtensionObject;
            TypeInfo typeInfo = TypeInfo.Construct(value);

            if (variable == null ||
                extensionObject == null ||
                typeInfo == null ||
                typeInfo == Opc.Ua.TypeInfo.Unknown)
            {
                return StatusCodes.BadTypeMismatch;
            }

            Range newRange = extensionObject.Body as Range;
            AnalogItemState parent = variable.Parent as AnalogItemState;
            if (newRange == null ||
                parent == null)
            {
                return StatusCodes.BadTypeMismatch;
            }

            if (indexRange != NumericRange.Empty)
            {
                return StatusCodes.BadIndexRangeInvalid;
            }

            TypeInfo parentTypeInfo = TypeInfo.Construct(parent.Value);
            Range parentRange = GetAnalogRange(parentTypeInfo.BuiltInType);
            if (parentRange.High < newRange.High ||
                parentRange.Low > newRange.Low)
            {
                return StatusCodes.BadOutOfRange;
            }

            value = newRange;

            return ServiceResult.Good;
        }

        /// <summary>
        /// Creates a new variable.
        /// </summary>
        private BaseDataVariableState CreateVariable(NodeState parent, string path, string name, BuiltInType dataType, int valueRank)
        {
            return CreateVariable(parent, path, name, (uint)dataType, valueRank);
        }

        /// <summary>
        /// Creates a new variable.
        /// </summary>
        private BaseDataVariableState CreateVariable(NodeState parent, string path, string name, NodeId dataType, int valueRank)
        {
            BaseDataVariableState variable = new BaseDataVariableState(parent);

            variable.SymbolicName = name;
            variable.ReferenceTypeId = ReferenceTypes.Organizes;
            variable.TypeDefinitionId = VariableTypeIds.BaseDataVariableType;
            variable.NodeId = new NodeId(path, NamespaceIndex);
            variable.BrowseName = new QualifiedName(path, NamespaceIndex);
            variable.DisplayName = new LocalizedText("en", name);
            variable.WriteMask = AttributeWriteMask.DisplayName | AttributeWriteMask.Description;
            variable.UserWriteMask = AttributeWriteMask.DisplayName | AttributeWriteMask.Description;
            variable.DataType = dataType;
            variable.ValueRank = valueRank;
            variable.AccessLevel = AccessLevels.CurrentReadOrWrite;
            variable.UserAccessLevel = AccessLevels.CurrentReadOrWrite;
            variable.Historizing = false;
            variable.Value = GetNewValue(variable);
            variable.StatusCode = StatusCodes.Good;
            variable.Timestamp = DateTime.UtcNow;

            if (valueRank == ValueRanks.OneDimension)
            {
                variable.ArrayDimensions = new ReadOnlyList<uint>(new List<uint> { 0 });
            }
            else if (valueRank == ValueRanks.TwoDimensions)
            {
                variable.ArrayDimensions = new ReadOnlyList<uint>(new List<uint> { 0, 0 });
            }

            if (parent != null)
            {
                parent.AddChild(variable);
            }

            return variable;
        }

       

        private object GetNewValue(BaseVariableState variable)
        {
            if (m_generator == null)
            {
                m_generator = new Opc.Ua.Test.DataGenerator(null);
                m_generator.BoundaryValueFrequency = 0;
            }

            object value = null;

            while (value == null)
            {
                value = m_generator.GetRandom(variable.DataType, variable.ValueRank, new uint[] { 10 }, Server.TypeTree);
            }

            return value;
        }

        private void DoSimulation(object state)
        {
            try
            {
                lock (Lock)
                {
                    foreach (BaseDataVariableState variable in m_dynamicNodes)
                    {
                        variable.Value = GetNewValue(variable);
                        variable.Timestamp = DateTime.UtcNow;
                        variable.ClearChangeMasks(SystemContext, false);
                    }
                }
            }
            catch (Exception e)
            {
                Utils.Trace(e, "Unexpected error doing simulation.");
            }
        }

        /// <summary>
        /// Frees any resources allocated for the address space.
        /// </summary>
        public override void DeleteAddressSpace()
        {
            lock (Lock)
            {
                // TBD
            }
        }

        /// <summary>
        /// Returns a unique handle for the node.
        /// </summary>
        protected override NodeHandle GetManagerHandle(ServerSystemContext context, NodeId nodeId, IDictionary<NodeId, NodeState> cache)
        {
            lock (Lock)
            {
                // quickly exclude nodes that are not in the namespace. 
                if (!IsNodeIdInNamespace(nodeId))
                {
                    return null;
                }

                NodeState node = null;

                if (!PredefinedNodes.TryGetValue(nodeId, out node))
                {
                    return null;
                }

                NodeHandle handle = new NodeHandle();

                handle.NodeId = nodeId;
                handle.Node = node;
                handle.Validated = true;

                return handle;
            }
        }

        /// <summary>
        /// Verifies that the specified node exists.
        /// </summary>
        protected override NodeState ValidateNode(
           ServerSystemContext context,
           NodeHandle handle,
           IDictionary<NodeId, NodeState> cache)
        {
            // not valid if no root.
            if (handle == null)
            {
                return null;
            }

            // check if previously validated.
            if (handle.Validated)
            {
                return handle.Node;
            }

            // TBD

            return null;
        }
        #endregion
        
        #region SharpNodeSettings Server

        private SharpNodeServer sharpNodeServer = null;
        private Dictionary<string, List<RegularItemNode>> dicRegularItemNode = null;
        private Dictionary<string, BaseDataVariableState> dict_BaseDataVariableState;    // 节点管理器

        #endregion

        #region Overrides
        #endregion

        #region Private Fields
        private ReferenceServerConfiguration m_configuration;
        private Opc.Ua.Test.DataGenerator m_generator;
        private Timer m_simulationTimer;
        private UInt16 m_simulationInterval = 1000;
        private bool m_simulationEnabled = true;
        private List<BaseDataVariableState> m_dynamicNodes;
        #endregion
    }
}
