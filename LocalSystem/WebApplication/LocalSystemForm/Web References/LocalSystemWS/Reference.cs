﻿//------------------------------------------------------------------------------
// <auto-generated>
//     此代码由工具生成。
//     运行时版本:2.0.50727.3615
//
//     对此文件的更改可能会导致不正确的行为，并且如果
//     重新生成代码，这些更改将会丢失。
// </auto-generated>
//------------------------------------------------------------------------------

// 
// 此源代码是由 Microsoft.VSDesigner 2.0.50727.3615 版自动生成。
// 
#pragma warning disable 1591

namespace LocalSystemForm.LocalSystemWS {
    using System.Diagnostics;
    using System.Web.Services;
    using System.ComponentModel;
    using System.Web.Services.Protocols;
    using System;
    using System.Xml.Serialization;
    
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "2.0.50727.3053")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Web.Services.WebServiceBindingAttribute(Name="SmartDeviceMgrWSSoap", Namespace="http://com.LocalSystem.Webservice")]
    [System.Xml.Serialization.XmlIncludeAttribute(typeof(EntityBase))]
    public partial class SmartDeviceMgrWS : System.Web.Services.Protocols.SoapHttpClientProtocol {
        
        private System.Threading.SendOrPostCallback LoadAppUserOperationCompleted;
        
        private System.Threading.SendOrPostCallback CheckAndLoadBarCodeOperationCompleted;
        
        private System.Threading.SendOrPostCallback CreateBarCodeOperationCompleted;
        
        private bool useDefaultCredentialsSetExplicitly;
        
        /// <remarks/>
        public SmartDeviceMgrWS() {
            this.Url = global::LocalSystemForm.Properties.Settings.Default.LocalSystemForm_LocalSystemWS_SmartDeviceMgrWS;
            if ((this.IsLocalFileSystemWebService(this.Url) == true)) {
                this.UseDefaultCredentials = true;
                this.useDefaultCredentialsSetExplicitly = false;
            }
            else {
                this.useDefaultCredentialsSetExplicitly = true;
            }
        }
        
        public new string Url {
            get {
                return base.Url;
            }
            set {
                if ((((this.IsLocalFileSystemWebService(base.Url) == true) 
                            && (this.useDefaultCredentialsSetExplicitly == false)) 
                            && (this.IsLocalFileSystemWebService(value) == false))) {
                    base.UseDefaultCredentials = false;
                }
                base.Url = value;
            }
        }
        
        public new bool UseDefaultCredentials {
            get {
                return base.UseDefaultCredentials;
            }
            set {
                base.UseDefaultCredentials = value;
                this.useDefaultCredentialsSetExplicitly = true;
            }
        }
        
        /// <remarks/>
        public event LoadAppUserCompletedEventHandler LoadAppUserCompleted;
        
        /// <remarks/>
        public event CheckAndLoadBarCodeCompletedEventHandler CheckAndLoadBarCodeCompleted;
        
        /// <remarks/>
        public event CreateBarCodeCompletedEventHandler CreateBarCodeCompleted;
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://com.LocalSystem.Webservice/LoadAppUser", RequestNamespace="http://com.LocalSystem.Webservice", ResponseNamespace="http://com.LocalSystem.Webservice", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public AppUser LoadAppUser(string userCode) {
            object[] results = this.Invoke("LoadAppUser", new object[] {
                        userCode});
            return ((AppUser)(results[0]));
        }
        
        /// <remarks/>
        public void LoadAppUserAsync(string userCode) {
            this.LoadAppUserAsync(userCode, null);
        }
        
        /// <remarks/>
        public void LoadAppUserAsync(string userCode, object userState) {
            if ((this.LoadAppUserOperationCompleted == null)) {
                this.LoadAppUserOperationCompleted = new System.Threading.SendOrPostCallback(this.OnLoadAppUserOperationCompleted);
            }
            this.InvokeAsync("LoadAppUser", new object[] {
                        userCode}, this.LoadAppUserOperationCompleted, userState);
        }
        
        private void OnLoadAppUserOperationCompleted(object arg) {
            if ((this.LoadAppUserCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.LoadAppUserCompleted(this, new LoadAppUserCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://com.LocalSystem.Webservice/CheckAndLoadBarCode", RequestNamespace="http://com.LocalSystem.Webservice", ResponseNamespace="http://com.LocalSystem.Webservice", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public BarCode CheckAndLoadBarCode(string barCode, string userCode) {
            object[] results = this.Invoke("CheckAndLoadBarCode", new object[] {
                        barCode,
                        userCode});
            return ((BarCode)(results[0]));
        }
        
        /// <remarks/>
        public void CheckAndLoadBarCodeAsync(string barCode, string userCode) {
            this.CheckAndLoadBarCodeAsync(barCode, userCode, null);
        }
        
        /// <remarks/>
        public void CheckAndLoadBarCodeAsync(string barCode, string userCode, object userState) {
            if ((this.CheckAndLoadBarCodeOperationCompleted == null)) {
                this.CheckAndLoadBarCodeOperationCompleted = new System.Threading.SendOrPostCallback(this.OnCheckAndLoadBarCodeOperationCompleted);
            }
            this.InvokeAsync("CheckAndLoadBarCode", new object[] {
                        barCode,
                        userCode}, this.CheckAndLoadBarCodeOperationCompleted, userState);
        }
        
        private void OnCheckAndLoadBarCodeOperationCompleted(object arg) {
            if ((this.CheckAndLoadBarCodeCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.CheckAndLoadBarCodeCompleted(this, new CheckAndLoadBarCodeCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://com.LocalSystem.Webservice/CreateBarCode", RequestNamespace="http://com.LocalSystem.Webservice", ResponseNamespace="http://com.LocalSystem.Webservice", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public void CreateBarCode(BarCode[] barCodes, string createUser) {
            this.Invoke("CreateBarCode", new object[] {
                        barCodes,
                        createUser});
        }
        
        /// <remarks/>
        public void CreateBarCodeAsync(BarCode[] barCodes, string createUser) {
            this.CreateBarCodeAsync(barCodes, createUser, null);
        }
        
        /// <remarks/>
        public void CreateBarCodeAsync(BarCode[] barCodes, string createUser, object userState) {
            if ((this.CreateBarCodeOperationCompleted == null)) {
                this.CreateBarCodeOperationCompleted = new System.Threading.SendOrPostCallback(this.OnCreateBarCodeOperationCompleted);
            }
            this.InvokeAsync("CreateBarCode", new object[] {
                        barCodes,
                        createUser}, this.CreateBarCodeOperationCompleted, userState);
        }
        
        private void OnCreateBarCodeOperationCompleted(object arg) {
            if ((this.CreateBarCodeCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.CreateBarCodeCompleted(this, new System.ComponentModel.AsyncCompletedEventArgs(invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        public new void CancelAsync(object userState) {
            base.CancelAsync(userState);
        }
        
        private bool IsLocalFileSystemWebService(string url) {
            if (((url == null) 
                        || (url == string.Empty))) {
                return false;
            }
            System.Uri wsUri = new System.Uri(url);
            if (((wsUri.Port >= 1024) 
                        && (string.Compare(wsUri.Host, "localHost", System.StringComparison.OrdinalIgnoreCase) == 0))) {
                return true;
            }
            return false;
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "2.0.50727.3082")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://com.LocalSystem.Webservice")]
    public partial class AppUser : AppUserBase {
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlIncludeAttribute(typeof(AppUser))]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "2.0.50727.3082")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://com.LocalSystem.Webservice")]
    public abstract partial class AppUserBase : EntityBase {
        
        private string codeField;
        
        private string addressField;
        
        private string emailField;
        
        private string firstNameField;
        
        private string lastNameField;
        
        private string passwordField;
        
        private string phoneField;
        
        private string faxField;
        
        private string languageField;
        
        private bool isActiveField;
        
        /// <remarks/>
        public string Code {
            get {
                return this.codeField;
            }
            set {
                this.codeField = value;
            }
        }
        
        /// <remarks/>
        public string Address {
            get {
                return this.addressField;
            }
            set {
                this.addressField = value;
            }
        }
        
        /// <remarks/>
        public string Email {
            get {
                return this.emailField;
            }
            set {
                this.emailField = value;
            }
        }
        
        /// <remarks/>
        public string FirstName {
            get {
                return this.firstNameField;
            }
            set {
                this.firstNameField = value;
            }
        }
        
        /// <remarks/>
        public string LastName {
            get {
                return this.lastNameField;
            }
            set {
                this.lastNameField = value;
            }
        }
        
        /// <remarks/>
        public string Password {
            get {
                return this.passwordField;
            }
            set {
                this.passwordField = value;
            }
        }
        
        /// <remarks/>
        public string Phone {
            get {
                return this.phoneField;
            }
            set {
                this.phoneField = value;
            }
        }
        
        /// <remarks/>
        public string Fax {
            get {
                return this.faxField;
            }
            set {
                this.faxField = value;
            }
        }
        
        /// <remarks/>
        public string Language {
            get {
                return this.languageField;
            }
            set {
                this.languageField = value;
            }
        }
        
        /// <remarks/>
        public bool IsActive {
            get {
                return this.isActiveField;
            }
            set {
                this.isActiveField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlIncludeAttribute(typeof(BarCodeBase))]
    [System.Xml.Serialization.XmlIncludeAttribute(typeof(BarCode))]
    [System.Xml.Serialization.XmlIncludeAttribute(typeof(AppUserBase))]
    [System.Xml.Serialization.XmlIncludeAttribute(typeof(AppUser))]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "2.0.50727.3082")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://com.LocalSystem.Webservice")]
    public abstract partial class EntityBase {
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlIncludeAttribute(typeof(BarCode))]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "2.0.50727.3082")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://com.LocalSystem.Webservice")]
    public abstract partial class BarCodeBase : EntityBase {
        
        private int idField;
        
        private System.Nullable<int> seqField;
        
        private string barCodeField;
        
        private string lotNoField;
        
        private string itemCodeField;
        
        private string itemDescriptionField;
        
        private string uomField;
        
        private System.Nullable<decimal> ucField;
        
        private decimal qtyField;
        
        private string statusField;
        
        private string supplierCodeField;
        
        private string createUserField;
        
        private System.Nullable<System.DateTime> createDateField;
        
        private string memoField;
        
        private System.Nullable<int> poDetailIdField;
        
        /// <remarks/>
        public int Id {
            get {
                return this.idField;
            }
            set {
                this.idField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true)]
        public System.Nullable<int> Seq {
            get {
                return this.seqField;
            }
            set {
                this.seqField = value;
            }
        }
        
        /// <remarks/>
        public string BarCode {
            get {
                return this.barCodeField;
            }
            set {
                this.barCodeField = value;
            }
        }
        
        /// <remarks/>
        public string LotNo {
            get {
                return this.lotNoField;
            }
            set {
                this.lotNoField = value;
            }
        }
        
        /// <remarks/>
        public string ItemCode {
            get {
                return this.itemCodeField;
            }
            set {
                this.itemCodeField = value;
            }
        }
        
        /// <remarks/>
        public string ItemDescription {
            get {
                return this.itemDescriptionField;
            }
            set {
                this.itemDescriptionField = value;
            }
        }
        
        /// <remarks/>
        public string Uom {
            get {
                return this.uomField;
            }
            set {
                this.uomField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true)]
        public System.Nullable<decimal> UC {
            get {
                return this.ucField;
            }
            set {
                this.ucField = value;
            }
        }
        
        /// <remarks/>
        public decimal Qty {
            get {
                return this.qtyField;
            }
            set {
                this.qtyField = value;
            }
        }
        
        /// <remarks/>
        public string Status {
            get {
                return this.statusField;
            }
            set {
                this.statusField = value;
            }
        }
        
        /// <remarks/>
        public string SupplierCode {
            get {
                return this.supplierCodeField;
            }
            set {
                this.supplierCodeField = value;
            }
        }
        
        /// <remarks/>
        public string CreateUser {
            get {
                return this.createUserField;
            }
            set {
                this.createUserField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true)]
        public System.Nullable<System.DateTime> CreateDate {
            get {
                return this.createDateField;
            }
            set {
                this.createDateField = value;
            }
        }
        
        /// <remarks/>
        public string Memo {
            get {
                return this.memoField;
            }
            set {
                this.memoField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true)]
        public System.Nullable<int> PoDetailId {
            get {
                return this.poDetailIdField;
            }
            set {
                this.poDetailIdField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "2.0.50727.3082")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://com.LocalSystem.Webservice")]
    public partial class BarCode : BarCodeBase {
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "2.0.50727.3053")]
    public delegate void LoadAppUserCompletedEventHandler(object sender, LoadAppUserCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "2.0.50727.3053")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class LoadAppUserCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal LoadAppUserCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public AppUser Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((AppUser)(this.results[0]));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "2.0.50727.3053")]
    public delegate void CheckAndLoadBarCodeCompletedEventHandler(object sender, CheckAndLoadBarCodeCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "2.0.50727.3053")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class CheckAndLoadBarCodeCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal CheckAndLoadBarCodeCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public BarCode Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((BarCode)(this.results[0]));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "2.0.50727.3053")]
    public delegate void CreateBarCodeCompletedEventHandler(object sender, System.ComponentModel.AsyncCompletedEventArgs e);
}

#pragma warning restore 1591