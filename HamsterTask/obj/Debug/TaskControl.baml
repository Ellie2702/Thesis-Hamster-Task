   M S B A M L   `   `   `  ����F  BHamsterTask, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null)clr-namespace:HamsterTaskHamsterTask  5          HamsterTask.TaskControl   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      +localclr-namespace:HamsterTask     5      $��123.5��5   3   .��5      � ��5	   
   �� 5
        �� EndPoint$  0.5,1&�6"    �� 
StartPoint$ 0.5,0&�63   .��5      �  � Color$ 	#3F7BD4F5��6    � Offset$ 0.897Q�61   � 5      $ 	#33DCF5FF��6   5       � ColumnDefinitions 5   
   �� 5      �� 5      �� 5      �� 5       � RowDefinitions 5   
   �� 5      �� 5      �� 5      �� 5      .+�5   
   �� -   5         ��Name$ TaskName��5      $��3��6"   $
��Name��66   $��12(�6E   �� 5    
   -   5        $ TaskID��5       $y�Hidden=�6    �� 5"   
   -   5        $ TaskExecutor��5"   -   $��2��6   $��0��6   $��4��6D      TaskExecutor#���   6X   $��12(�6�   $	��Top=�6�   �� 5#   
   -   5        $ deadLine��5#   -   $��1��6   $��0��6   $��3��6?     TaskDeadline#���  6S   $��12(�6|   $��Center=�6�   �� 5$   
   -   5        $ Owner��5$   -   $��3��6   $��0��6     	TaskOwner#���  6<   $��4��6b   $��12(�6v   $��Center=�6�   �� 5%   
   -   5         ��Name$
 Done��5%      $y�Hidden=�6   $��/Image/tick.png��62   $��3��6K   $
��Left=�6[   $��32��6v   $��2,0,0,0q�6�   $	��Top=�6�   $��34��6�   �� 5&   
   -   5        $
��Show��5&      $��3��6    $
��Left=�60   $��41,5,0,0q�6K   $	��Top=�6]   $��29��6u   $��24��6�   ��6�   Z� ��6�   Z� ��5'      �� 5(       �� ImageSource$ Image/search.png��6   5*      5+      5,      