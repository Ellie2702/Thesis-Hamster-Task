   M S B A M L   `   `   `  ����F  BHamsterTask, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null)clr-namespace:HamsterTaskHamsterTask  5   	       HamsterTask.Department   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   +localclr-namespace:HamsterTask     5   	    	  deprw  � Title#  �   5   	   $	��399��6)   $��609.263��66    � WindowStartupLocation$ CenterScreen=�6F   .��5	      � -   5         � ColumnDefinitions 5
   
   �� 5      �� 5      �� 5       � RowDefinitions 5   
   �� 5      �� 5      �� 5      �� 5      �� 5      .+�5   
   �� $��0��6   $��3��6%   $��4��69   $��1��6G   ��5      �� 5       �� EndPoint$ 0.5,1&�6&    � MappingMode$ RelativeToBoundingBox=�67    �� 
StartPoint$ 0.5,0&�6[   .��5      �  � Color$ White��6#   � 5      $ 	#FF031419��6#    � Offset$ 0.989Q�65   � 5      $ 	#FF126A7A��6#   $ 0.342Q�65   � 5      $ 	#FF579CB6��6#   $ 0.232Q�65   � 5      $ 	#FF0E6376��6#   $ 0.644Q�65   5      5!      �� 5"   
   -   5        	 ��Name$	 DepName��5"      $��18(�6!   $��0��6/   $��0��6?   $��2��6L   $��Center=�6`   $��
10,5,10,10q�6�   ��5#      �� 5$      $ 0.5,1&�6&   $ 0.5,0&�67   .��5%      � $ 	#72BBE284��6#   $ 0Q�65   � 5&      $ 	#728BECE3��6#   $ 1Q�65   5'      5)      �� 5*   
   -   5        
 ��Name$
 RemEmp��5*      $��2��6!   $
��Left=�61   $��
136,10,0,0q�6L   $	��Top=�6`   $��54��6x   $��42��6�   ��6�   Z� ��6�   Z� ��5+      �� 5,       �� ImageSource$ Image/delete.png��6   5.      �� 5/   
   -   5        $

 Edit��5/      $��2��6   $
��Left=�6/   $	��Top=�6J   $��54��6b   $��42��6m   $��	62,10,0,0q�6�    J� RenderTransformOrigin$ -0.986,-0.098&�6�   ��6y   Z� ��6�   Z� ��50      �� 51      $ Image/Edit.png��6   53      �� 54   
   $��	10,10,0,0q�6   $��1��6*   $��3��67   $��2��6H   .��55      �� -   5         ��Name$ EmpsComp��55      $
��Left=�6+   $	��185��6F   $��	10,10,0,0q�6S   $��1��6f   $��3��6s   $	��Top=�6�   $	��381��6�   $��2��6�   56      �� 57   
   -   5         ��Name$
 mail��57      $��14(�6    $��10,34,49,10q�6.   $��2��6C   $��1��6S   �� 58   
   -   5        $
 	pastemail��58      $��2��6$   $
��Left=�64   $��
157,34,0,0q�6O   $	��Top=�6c   $��33��6{   $��30��6�   $ 0.682,0.806&�6�   $��1��6�   ��6�   Z� ��6�   Z� ��59      �� 5:      $ Image/lnput.png��6    � ToolTip 5<      _� 5=      .��5>      ��   AdminCopyTOOL#���  6    $
��Wrap=�6G   5?      5A      �� 5B   
   -   5        $

 Back��5B       
 AddEmp#���  6   $��2��6B   $��10,10,49,24q�6R   $��2��6~   ��6g   Z� ��5C      �� 5D      $ 0.5,1&�6&   $ RelativeToBoundingBox=�67   $ 0.5,0&�6[   .��5E      � $ 	#FFF4FAFF��6#   � 5F      $ 	#FFBCEAFF��6#   $ 0.608Q�65   � 5G      $ 	#FF60B4DA��6#   $ 0.919Q�65   5H      5J      5K      5L      