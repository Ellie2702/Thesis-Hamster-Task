﻿#pragma checksum "..\..\AddTask.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "371CDD7B838191B23813C8063BD98BDFECE856845C1CB931C523B43FC5BD2176"
//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан программой.
//     Исполняемая версия:4.0.30319.42000
//
//     Изменения в этом файле могут привести к неправильной работе и будут потеряны в случае
//     повторной генерации кода.
// </auto-generated>
//------------------------------------------------------------------------------

using HamsterTask;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Media.TextFormatting;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Shell;


namespace HamsterTask {
    
    
    /// <summary>
    /// AddTask
    /// </summary>
    public partial class AddTask : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 26 "..\..\AddTask.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label AddTaskDescript;
        
        #line default
        #line hidden
        
        
        #line 30 "..\..\AddTask.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox TaskName;
        
        #line default
        #line hidden
        
        
        #line 45 "..\..\AddTask.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label TaskExecSel;
        
        #line default
        #line hidden
        
        
        #line 48 "..\..\AddTask.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.CheckBox Me;
        
        #line default
        #line hidden
        
        
        #line 51 "..\..\AddTask.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox TaskDesc;
        
        #line default
        #line hidden
        
        
        #line 53 "..\..\AddTask.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DatePicker TaskDeadline;
        
        #line default
        #line hidden
        
        
        #line 54 "..\..\AddTask.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button AddBTN;
        
        #line default
        #line hidden
        
        
        #line 63 "..\..\AddTask.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button BTNCancel;
        
        #line default
        #line hidden
        
        
        #line 72 "..\..\AddTask.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox MailExec;
        
        #line default
        #line hidden
        
        
        #line 73 "..\..\AddTask.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button pastemail;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/HamsterTask;component/addtask.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\AddTask.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            
            #line 9 "..\..\AddTask.xaml"
            ((System.Windows.Controls.Grid)(target)).Loaded += new System.Windows.RoutedEventHandler(this.Grid_Loaded);
            
            #line default
            #line hidden
            return;
            case 2:
            this.AddTaskDescript = ((System.Windows.Controls.Label)(target));
            return;
            case 3:
            this.TaskName = ((System.Windows.Controls.TextBox)(target));
            return;
            case 4:
            this.TaskExecSel = ((System.Windows.Controls.Label)(target));
            return;
            case 5:
            this.Me = ((System.Windows.Controls.CheckBox)(target));
            return;
            case 6:
            this.TaskDesc = ((System.Windows.Controls.TextBox)(target));
            return;
            case 7:
            this.TaskDeadline = ((System.Windows.Controls.DatePicker)(target));
            return;
            case 8:
            this.AddBTN = ((System.Windows.Controls.Button)(target));
            
            #line 54 "..\..\AddTask.xaml"
            this.AddBTN.Click += new System.Windows.RoutedEventHandler(this.AddBTN_Click);
            
            #line default
            #line hidden
            return;
            case 9:
            this.BTNCancel = ((System.Windows.Controls.Button)(target));
            
            #line 63 "..\..\AddTask.xaml"
            this.BTNCancel.Click += new System.Windows.RoutedEventHandler(this.BTNCancel_Click);
            
            #line default
            #line hidden
            return;
            case 10:
            this.MailExec = ((System.Windows.Controls.TextBox)(target));
            return;
            case 11:
            this.pastemail = ((System.Windows.Controls.Button)(target));
            
            #line 73 "..\..\AddTask.xaml"
            this.pastemail.Click += new System.Windows.RoutedEventHandler(this.pastemail_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

