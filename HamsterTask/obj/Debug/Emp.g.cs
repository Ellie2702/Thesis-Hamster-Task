﻿#pragma checksum "..\..\Emp.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "BB389E2A4CFBD661BE43A0A095BE720F43FE2F0481EFC0F4E684E375251541D6"
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
    /// Emp
    /// </summary>
    public partial class Emp : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 37 "..\..\Emp.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label EmpName;
        
        #line default
        #line hidden
        
        
        #line 45 "..\..\Emp.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label EmpPos;
        
        #line default
        #line hidden
        
        
        #line 53 "..\..\Emp.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label EmpWork;
        
        #line default
        #line hidden
        
        
        #line 61 "..\..\Emp.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button CopyMail;
        
        #line default
        #line hidden
        
        
        #line 71 "..\..\Emp.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button RemEmp;
        
        #line default
        #line hidden
        
        
        #line 76 "..\..\Emp.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button EditPos;
        
        #line default
        #line hidden
        
        
        #line 81 "..\..\Emp.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label Department;
        
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
            System.Uri resourceLocater = new System.Uri("/HamsterTask;component/emp.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\Emp.xaml"
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
            
            #line 9 "..\..\Emp.xaml"
            ((System.Windows.Controls.Grid)(target)).Loaded += new System.Windows.RoutedEventHandler(this.Grid_Loaded);
            
            #line default
            #line hidden
            return;
            case 2:
            this.EmpName = ((System.Windows.Controls.Label)(target));
            return;
            case 3:
            this.EmpPos = ((System.Windows.Controls.Label)(target));
            return;
            case 4:
            this.EmpWork = ((System.Windows.Controls.Label)(target));
            return;
            case 5:
            this.CopyMail = ((System.Windows.Controls.Button)(target));
            
            #line 61 "..\..\Emp.xaml"
            this.CopyMail.Click += new System.Windows.RoutedEventHandler(this.CopyMail_Click);
            
            #line default
            #line hidden
            return;
            case 6:
            this.RemEmp = ((System.Windows.Controls.Button)(target));
            
            #line 71 "..\..\Emp.xaml"
            this.RemEmp.Click += new System.Windows.RoutedEventHandler(this.RemEmp_Click);
            
            #line default
            #line hidden
            return;
            case 7:
            this.EditPos = ((System.Windows.Controls.Button)(target));
            
            #line 76 "..\..\Emp.xaml"
            this.EditPos.Click += new System.Windows.RoutedEventHandler(this.EditPos_Click);
            
            #line default
            #line hidden
            return;
            case 8:
            this.Department = ((System.Windows.Controls.Label)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}

