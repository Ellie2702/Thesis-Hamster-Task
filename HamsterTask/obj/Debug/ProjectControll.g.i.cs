// Updated by XamlIntelliSenseFileGenerator 02.06.2020 12:30:01
#pragma checksum "..\..\ProjectControll.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "241D5DF33D8A1ADB4705BBF009CBD217C366E3DD009800D96023637752B41713"
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


namespace HamsterTask
{


    /// <summary>
    /// ProjectControll
    /// </summary>
    public partial class ProjectControll : System.Windows.Controls.UserControl, System.Windows.Markup.IComponentConnector
    {


#line 29 "..\..\ProjectControll.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label ProjectName;

#line default
#line hidden


#line 30 "..\..\ProjectControll.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label ProjectID;

#line default
#line hidden


#line 33 "..\..\ProjectControll.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock ProjectDesc;

#line default
#line hidden


#line 37 "..\..\ProjectControll.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button ViewProj;

#line default
#line hidden

        private bool _contentLoaded;

        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        public void InitializeComponent()
        {
            if (_contentLoaded)
            {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/HamsterTask;component/projectcontroll.xaml", System.UriKind.Relative);

#line 1 "..\..\ProjectControll.xaml"
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
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target)
        {
            switch (connectionId)
            {
                case 1:
                    this.ProjectName = ((System.Windows.Controls.Label)(target));
                    return;
                case 2:
                    this.ProjectID = ((System.Windows.Controls.Label)(target));
                    return;
                case 3:
                    this.ProjectDesc = ((System.Windows.Controls.TextBlock)(target));
                    return;
                case 4:
                    this.ViewProj = ((System.Windows.Controls.Button)(target));
                    return;
            }
            this._contentLoaded = true;
        }

        internal System.Windows.Controls.Label TasksDone;
        internal System.Windows.Controls.Label Deadline;
        internal System.Windows.Controls.Label DateDeadline;
    }
}
