﻿#pragma checksum "..\..\AdminPanel.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "148C5CD96248C01BFA80969CA237F788245B11D3C67A696708EFB439C109BAE0"
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
    /// AdminPanel
    /// </summary>
    public partial class AdminPanel : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 25 "..\..\AdminPanel.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label AdminName;
        
        #line default
        #line hidden
        
        
        #line 38 "..\..\AdminPanel.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button AddAdmin;
        
        #line default
        #line hidden
        
        
        #line 48 "..\..\AdminPanel.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock ToolTopUserTask;
        
        #line default
        #line hidden
        
        
        #line 52 "..\..\AdminPanel.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button DeleteCompany;
        
        #line default
        #line hidden
        
        
        #line 62 "..\..\AdminPanel.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock ToolTopUserTask1;
        
        #line default
        #line hidden
        
        
        #line 66 "..\..\AdminPanel.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button DeleteUser;
        
        #line default
        #line hidden
        
        
        #line 76 "..\..\AdminPanel.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock ToolTopUserTask2;
        
        #line default
        #line hidden
        
        
        #line 80 "..\..\AdminPanel.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button Messages;
        
        #line default
        #line hidden
        
        
        #line 90 "..\..\AdminPanel.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock ToolTopUserTask3;
        
        #line default
        #line hidden
        
        
        #line 94 "..\..\AdminPanel.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button Exit;
        
        #line default
        #line hidden
        
        
        #line 104 "..\..\AdminPanel.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock ToolTopUserTask4;
        
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
            System.Uri resourceLocater = new System.Uri("/HamsterTask;component/adminpanel.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\AdminPanel.xaml"
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
            
            #line 9 "..\..\AdminPanel.xaml"
            ((System.Windows.Controls.Grid)(target)).Loaded += new System.Windows.RoutedEventHandler(this.Grid_Loaded);
            
            #line default
            #line hidden
            return;
            case 2:
            this.AdminName = ((System.Windows.Controls.Label)(target));
            return;
            case 3:
            this.AddAdmin = ((System.Windows.Controls.Button)(target));
            
            #line 38 "..\..\AdminPanel.xaml"
            this.AddAdmin.Click += new System.Windows.RoutedEventHandler(this.AddAdmin_Click);
            
            #line default
            #line hidden
            return;
            case 4:
            this.ToolTopUserTask = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 5:
            this.DeleteCompany = ((System.Windows.Controls.Button)(target));
            
            #line 52 "..\..\AdminPanel.xaml"
            this.DeleteCompany.Click += new System.Windows.RoutedEventHandler(this.DeleteCompany_Click);
            
            #line default
            #line hidden
            return;
            case 6:
            this.ToolTopUserTask1 = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 7:
            this.DeleteUser = ((System.Windows.Controls.Button)(target));
            
            #line 66 "..\..\AdminPanel.xaml"
            this.DeleteUser.Click += new System.Windows.RoutedEventHandler(this.DeleteUser_Click);
            
            #line default
            #line hidden
            return;
            case 8:
            this.ToolTopUserTask2 = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 9:
            this.Messages = ((System.Windows.Controls.Button)(target));
            
            #line 80 "..\..\AdminPanel.xaml"
            this.Messages.Click += new System.Windows.RoutedEventHandler(this.Messages_Click);
            
            #line default
            #line hidden
            return;
            case 10:
            this.ToolTopUserTask3 = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 11:
            this.Exit = ((System.Windows.Controls.Button)(target));
            
            #line 94 "..\..\AdminPanel.xaml"
            this.Exit.Click += new System.Windows.RoutedEventHandler(this.Exit_Click);
            
            #line default
            #line hidden
            return;
            case 12:
            this.ToolTopUserTask4 = ((System.Windows.Controls.TextBlock)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}

