   M S B A M L   `   `   `  ����K  GDesktopInterface, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null?$clr-namespace:DesktopInterface.ViewsDesktopInterface.Views  5   	   '    !DesktopInterface.Views.WindowView   5   	   W SPresentationCore, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   6local$clr-namespace:DesktopInterface.Views     5   	     � Title$  
WindowView��5   	   $	��450��6   $	��800��6)   .��5	      �  � ColumnDefinitions 5
   
   �� 5      $��20 �6   �� 5      $
��auto �6   �� 5      $
��auto �6   �� 5      $
��auto �6   �� 5      $��* �6   �� 5      $��20 �6    � RowDefinitions 5   
   �� 5      $��20 �6   �� 5      $
��auto �6   �� 5      $��* �6   �� 5      $��20 �6   .+�5   
   �� -   5         ��Name$ LoadEntryView��5      $��1��6(   $��1��65   .��6E   	EntryView6P   �� 5   
   -   5        $ LoadDataGridView��5      $��1��6+   $��2��68   .��6H   Data6N   �� 5   
   -   5        $ LoadChartView��5      $��1��6(   $��3��65   .��6E   Chart6L   �� 5   
   -   5         ��Name$ 
ActiveItem��5      $��2��6-   $��1��6:   $��4��6J   5       5!      