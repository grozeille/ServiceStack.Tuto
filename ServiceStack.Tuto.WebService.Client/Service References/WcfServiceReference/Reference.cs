﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.1
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ServiceStack.Tuto.WebService.Client.WcfServiceReference {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(Namespace="http://grozeille.com/types", ConfigurationName="WcfServiceReference.ISyncReply")]
    public interface ISyncReply {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://grozeille.com/types/Add", ReplyAction="*")]
        [return: System.ServiceModel.MessageParameterAttribute(Name="Data")]
        ServiceStack.Tuto.WebService.Common.Operations.AddResponseData Add(int A, int B);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface ISyncReplyChannel : ServiceStack.Tuto.WebService.Client.WcfServiceReference.ISyncReply, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class SyncReplyClient : System.ServiceModel.ClientBase<ServiceStack.Tuto.WebService.Client.WcfServiceReference.ISyncReply>, ServiceStack.Tuto.WebService.Client.WcfServiceReference.ISyncReply {
        
        public SyncReplyClient() {
        }
        
        public SyncReplyClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public SyncReplyClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public SyncReplyClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public SyncReplyClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public ServiceStack.Tuto.WebService.Common.Operations.AddResponseData Add(int A, int B) {
            return base.Channel.Add(A, B);
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(Namespace="http://grozeille.com/types", ConfigurationName="WcfServiceReference.IOneWay")]
    public interface IOneWay {
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IOneWayChannel : ServiceStack.Tuto.WebService.Client.WcfServiceReference.IOneWay, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class OneWayClient : System.ServiceModel.ClientBase<ServiceStack.Tuto.WebService.Client.WcfServiceReference.IOneWay>, ServiceStack.Tuto.WebService.Client.WcfServiceReference.IOneWay {
        
        public OneWayClient() {
        }
        
        public OneWayClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public OneWayClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public OneWayClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public OneWayClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
    }
}
