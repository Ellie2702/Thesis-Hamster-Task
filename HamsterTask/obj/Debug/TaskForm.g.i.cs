﻿#pragma checksum "..\..\TaskForm.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "A5C03F9895D07F98CBBC473BC3E89DDD16D4DB6B6A3239366AF77B30710235BE"
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
    /// TaskForm
    /// </summary>
    public partial class TaskForm : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 26 "..\..\TaskForm.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label TaskName;
        
        #line default
        #line hidden
        
        
        #line 34 "..\..\TaskForm.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label Deadline;
        
        #line default
        #line hidden
        
        
        #line 35 "..\..\TaskForm.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label Exec;
        
        #line default
        #line hidden
        
        
        #line 36 "..\..\TaskForm.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label Own;
        
        #line default
        #line hidden
        
        
        #line 37 "..\..\TaskForm.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label TaskFailed;
        
        #line default
        #line hidden
        
        
        #line 39 "..\..\TaskForm.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ScrollViewer scrollDesc;
        
        #line default
        #line hidden
        
        
        #line 40 "..\..\TaskForm.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock TaskDesk;
        
        #line default
        #line hidden
        
        
        #line 50 "..\..\TaskForm.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ScrollViewer scrollFiles;
        
        #line default
        #line hidden
        
        
        #line 51 "..\..\TaskForm.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ListBox Files;
        
        #line default
        #line hidden
        
        
        #line 64 "..\..\TaskForm.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button RemTask;
        
        #line default
        #line hidden
        
        
        #line 69 "..\..\TaskForm.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button IsDone;
        
        #line default
        #line hidden
        
        
        #line 74 "..\..\TaskForm.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Image Done;
        
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
            System.Uri resourceLocater = new System.Uri("/HamsterTask;component/taskform.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\TaskForm.xaml"
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
            
            #line 9 "..\..\TaskForm.xaml"
            ((System.Windows.Controls.Grid)(target)).Loaded += new System.Windows.RoutedEventHandler(this.Grid_Loaded);
            
            #line default
            #line hidden
            return;
            case 2:
            this.TaskName = ((System.Windows.Controls.Label)(target));
            return;
            case 3:
            this.Deadline = ((System.Windows.Controls.Label)(target));
            return;
            case 4:
            this.Exec = ((System.Windows.Controls.Label)(target));
            return;
            case 5:
            this.Own = ((System.Windows.Controls.Label)(target));
            return;
            case 6:
            this.TaskFailed = ((System.Windows.Controls.Label)(target));
            return;
            case 7:
            this.scrollDesc = ((System.Windows.Controls.ScrollViewer)(target));
            return;
            case 8:
            this.TaskDesk = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 9:
            this.scrollFiles = ((System.Windows.Controls.ScrollViewer)(target));
            return;
            case 10:
            this.Files = ((System.Windows.Controls.ListBox)(target));
            return;
            case 11:
            this.RemTask = ((System.Windows.Controls.Button)(target));
            
            #line 64 "..\..\TaskForm.xaml"
            this.RemTask.Click += new System.Windows.RoutedEventHandler(this.RemTask_Click);
            
            #line default
            #line hidden
            return;
            case 12:
            this.IsDone = ((System.Windows.Controls.Button)(target));
            
            #line 69 "..\..\TaskForm.xaml"
            this.IsDone.Click += new System.Windows.RoutedEventHandler(this.IsDone_Click);
            
            #line default
            #line hidden
            return;
            case 13:
            this.Done = ((System.Windows.Controls.Image)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}

