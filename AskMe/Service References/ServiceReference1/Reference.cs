﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace AskMe.ServiceReference1 {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="ServiceReference1.SolrSearchServiceSoap")]
    public interface SolrSearchServiceSoap {
        
        // CODEGEN: Generating message contract since element name keyword from namespace http://tempuri.org/ is not marked nillable
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/QuerySolr", ReplyAction="*")]
        AskMe.ServiceReference1.QuerySolrResponse QuerySolr(AskMe.ServiceReference1.QuerySolrRequest request);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/QuerySolr", ReplyAction="*")]
        System.Threading.Tasks.Task<AskMe.ServiceReference1.QuerySolrResponse> QuerySolrAsync(AskMe.ServiceReference1.QuerySolrRequest request);
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class QuerySolrRequest {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="QuerySolr", Namespace="http://tempuri.org/", Order=0)]
        public AskMe.ServiceReference1.QuerySolrRequestBody Body;
        
        public QuerySolrRequest() {
        }
        
        public QuerySolrRequest(AskMe.ServiceReference1.QuerySolrRequestBody Body) {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="http://tempuri.org/")]
    public partial class QuerySolrRequestBody {
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=0)]
        public string keyword;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=1)]
        public string notKeyword;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=2)]
        public string exact;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=3)]
        public string rows;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=4)]
        public string filter;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=5)]
        public string fuzzy;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=6)]
        public string highlight;
        
        public QuerySolrRequestBody() {
        }
        
        public QuerySolrRequestBody(string keyword, string notKeyword, string exact, string rows, string filter, string fuzzy, string highlight) {
            this.keyword = keyword;
            this.notKeyword = notKeyword;
            this.exact = exact;
            this.rows = rows;
            this.filter = filter;
            this.fuzzy = fuzzy;
            this.highlight = highlight;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class QuerySolrResponse {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="QuerySolrResponse", Namespace="http://tempuri.org/", Order=0)]
        public AskMe.ServiceReference1.QuerySolrResponseBody Body;
        
        public QuerySolrResponse() {
        }
        
        public QuerySolrResponse(AskMe.ServiceReference1.QuerySolrResponseBody Body) {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="http://tempuri.org/")]
    public partial class QuerySolrResponseBody {
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=0)]
        public System.Xml.Linq.XElement QuerySolrResult;
        
        public QuerySolrResponseBody() {
        }
        
        public QuerySolrResponseBody(System.Xml.Linq.XElement QuerySolrResult) {
            this.QuerySolrResult = QuerySolrResult;
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface SolrSearchServiceSoapChannel : AskMe.ServiceReference1.SolrSearchServiceSoap, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class SolrSearchServiceSoapClient : System.ServiceModel.ClientBase<AskMe.ServiceReference1.SolrSearchServiceSoap>, AskMe.ServiceReference1.SolrSearchServiceSoap {
        
        public SolrSearchServiceSoapClient() {
        }
        
        public SolrSearchServiceSoapClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public SolrSearchServiceSoapClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public SolrSearchServiceSoapClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public SolrSearchServiceSoapClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        AskMe.ServiceReference1.QuerySolrResponse AskMe.ServiceReference1.SolrSearchServiceSoap.QuerySolr(AskMe.ServiceReference1.QuerySolrRequest request) {
            return base.Channel.QuerySolr(request);
        }
        
        public System.Xml.Linq.XElement QuerySolr(string keyword, string notKeyword, string exact, string rows, string filter, string fuzzy, string highlight) {
            AskMe.ServiceReference1.QuerySolrRequest inValue = new AskMe.ServiceReference1.QuerySolrRequest();
            inValue.Body = new AskMe.ServiceReference1.QuerySolrRequestBody();
            inValue.Body.keyword = keyword;
            inValue.Body.notKeyword = notKeyword;
            inValue.Body.exact = exact;
            inValue.Body.rows = rows;
            inValue.Body.filter = filter;
            inValue.Body.fuzzy = fuzzy;
            inValue.Body.highlight = highlight;
            AskMe.ServiceReference1.QuerySolrResponse retVal = ((AskMe.ServiceReference1.SolrSearchServiceSoap)(this)).QuerySolr(inValue);
            return retVal.Body.QuerySolrResult;
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        System.Threading.Tasks.Task<AskMe.ServiceReference1.QuerySolrResponse> AskMe.ServiceReference1.SolrSearchServiceSoap.QuerySolrAsync(AskMe.ServiceReference1.QuerySolrRequest request) {
            return base.Channel.QuerySolrAsync(request);
        }
        
        public System.Threading.Tasks.Task<AskMe.ServiceReference1.QuerySolrResponse> QuerySolrAsync(string keyword, string notKeyword, string exact, string rows, string filter, string fuzzy, string highlight) {
            AskMe.ServiceReference1.QuerySolrRequest inValue = new AskMe.ServiceReference1.QuerySolrRequest();
            inValue.Body = new AskMe.ServiceReference1.QuerySolrRequestBody();
            inValue.Body.keyword = keyword;
            inValue.Body.notKeyword = notKeyword;
            inValue.Body.exact = exact;
            inValue.Body.rows = rows;
            inValue.Body.filter = filter;
            inValue.Body.fuzzy = fuzzy;
            inValue.Body.highlight = highlight;
            return ((AskMe.ServiceReference1.SolrSearchServiceSoap)(this)).QuerySolrAsync(inValue);
        }
    }
}