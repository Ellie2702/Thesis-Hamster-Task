   M S B A M L   `   `   `  ����F  BHamsterTask, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null)clr-namespace:HamsterTaskHamsterTask  5   	   !    HamsterTask.RegistrationOrg   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   +localclr-namespace:HamsterTask     5   	      HamsterTask.RegistrationOrg  � Title#  �   5   	   $	��550��6?   $	��800��6L    � WindowStartupLocation$ CenterScreen=�6X    � 	Resources 5	      :  4System.Windows.Controls.Primitives.DatePickerTextBox �� 
TargetType �� Property �� Value��%Z   '        �� 5
   
   "  6   .�5      ��  � ��$ 
01.01.2001��6%   5      5      .��5      �  � ColumnDefinitions 5   
   �� 5      �� 5      �� 5      �� 5       � RowDefinitions 5   
   �� 5      $	��75* �6   �� 5      $��75 �6   �� 5      $��71 �6   �� 5      $��79 �6   �� 5      $��71 �6   �� 5      $��74 �6   �� 5      $	��74* �6   .+�5    
   �� -   5         ��Name$ LabelCompanyName��5         LabelCompanyName#���  6*   $��2��6W   $��20(�6d   $��Center=�6r   $��Right=�6�   �� 5!   
   -   5        $��CompanyName��5!      $��2��6&   $��1��63   $��
5, 25,5,15q�6C   $��20(�6W   �� 5#   
   -   5        $ LabelCompanyType��5#        LabelCompanyType#���  6*   $��3��6W   $��20(�6d   $��Center=�6r   $��Right=�6�   �� 5$   
   -   5        $��CompanyType��5$      $��3��6&   $��1��63   $��	5,25,5,24q�6C   $��20(�6V   �� 5&   
   -   5        $ LabelCompanyBirth��5&        LabelCompanyBirth#���  6+   $��4��6Y   $��20(�6f   $��Center=�6t   $��Right=�6�   (  "System.Windows.Controls.DatePicker  5'   
   -   5        	  Name$	 CompanyBirth��5'      $��4��6-   $��1��6:   $��20(�6J   $��Center=�6X   $��34��6s   $��	0,20,0,17q�6   5)      �� 5+   
   -   5        $ LabelCompanyRegDescript��5+      $��1��61   $��0��6>   $��3��6N   $��Bottom=�6b   $��70,0,5,0q�6�   $��70��6�   $	��Top=�6�   .��5,      �� $
��Wrap=�6   $��15(�6,     LabelCompanyRegDescript#���  6;   $��65��6l   $	��509��6x   6�   5-      �� 5/   
   -   5        
 ��Name$
 BtnContinueNoReg��5/      $��2��6+   $��4��6;   $��10q�6H   ��6s   Z� ��50      �� 51       �� EndPoint$ 0.5,1&�6&    � MappingMode$ RelativeToBoundingBox=�67    �� 
StartPoint$ 0.5,0&�6[   .��52      �  � Color$ 	#FFF4FAFF��6#   � 53      $ 	#FFBCEAFF��6#    � Offset$ 0.608Q�65   � 54      $ 	#FF60B4DA��6#   $ 0.919Q�65   55      .��57      �� $
��Wrap=�6     BtnContinueNoReg#���  6,   6X   58      �� 5:   
   -	   5        $
 BTNBackToMain��5:        BTNBackToMain#���  6(   $��3��6R   $��4��6b   $��
10,7,10,10q�6o   ��6�   Z� ��5;      �� 5<      $ 0.5,1&�6&   $ RelativeToBoundingBox=�67   $ 0.5,0&�6[   .��5=      � $ 	#FFF4FAFF��6#   � 5>      $ 	#FFBCEAFF��6#   $ 0.608Q�65   � 5?      $ 	#FF60B4DA��6#   $ 0.919Q�65   5@      5B      �� 5D   
   $��0��6   $��4��6%   $��6��69   ��5E      �� 5F      $ 0.5,1&�6&   $ RelativeToBoundingBox=�67   $ 0.5,0&�6[   .��5G      � $ White��6#   � 5H      $ 	#FF031419��6#   $ 0.989Q�65   � 5I      $ 	#FF126A7A��6#   $ 0.539Q�65   � 5J      $ 	#FF579CB6��6#   $ 0.401Q�65   � 5K      $ 	#FF0E6376��6#   $ 0.644Q�65   5L      5N      5O      5P      