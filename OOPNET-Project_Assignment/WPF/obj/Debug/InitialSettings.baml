   M S B A M L   `   `   `  ����>  :WPF, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullclr-namespace:WPFWPF  5   	       WPF.InitialSettings   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   #localclr-namespace:WPF     5   	   -   5          � Title$  Initial Settings��5   	   $	��250��6"   $	��350��62   $	��250��6A   $	��350��6N   $��Stretch=�6Z   $��Stretch=�6    � WindowStartupLocation$ CenterScreen=�6�   .��5	      �  � ColumnDefinitions 5
   
   �� 5      �� 5      $	��69* �6   .+�5   
   �� -   5         ��Name$ lblLanguage��5      $��Choose language: ��6%   $��5,5,5,5q�6A   $��2��6R   �� 5   
   -   5         ��Name$ LanguageComboBox��5      $
��Left=�6-   $	��Top=�6H   $	��200��6`   $��
115,10,0,0q�6l   $��1��6�   .@�5      �� $��English��6   �� 5      $��Croatian��6   5      �� 5   
   -   5        $ 	lblGender��5      $��Choose gender: ��6#   $��	5,45,5,45q�6=   $��2��6P   �� 5   
   -   5        $ GenderComboBox��5      $
��Left=�6+   $	��Top=�6F   $	��200��6^   $��
115,50,0,0q�6j   $��1��6~   .@�5      �� $
��Male��6   �� 5      $��Female��6   5      �� 5   
   -   5        $ lblResolution��5      $��Choose resolution: ��6'   $��	5,85,5,85q�6E   $��2��6X   �� 5   
   -   5        $ ResolutionComboBox��5      $
��Left=�6/   $	��Top=�6J   $	��200��6b   $��
115,90,0,0q�6n   $��1��6�   .@�5      �� $��	1920x1080��6   �� 5      -   5        $��1280x720��5      6.   �� 5      $��800x630��6   5      �� 5   
   -	   5         ��Name$ 
cbReadMode��5      $��:Read from file mode��6'   $��	5,130,0,0q�6F   $��Stretch=�6Y   $��RightToLeft=�6~   $
��Left=�6�   $��1��6�   �� 5   
   -
   5         ��Name$ btnSave��5      $
��Save��6"   $
��Left=�61   $	��Top=�6L   $	��100��6d   $��125,160,0,0q�6p   $��1��6�    �� 	IsDefault . 6�   5       5!      