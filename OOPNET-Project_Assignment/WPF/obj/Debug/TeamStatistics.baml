   M S B A M L   `   `   `  ����>  :WPF, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullclr-namespace:WPFWPF  5   	       WPF.TeamStatistics   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   #localclr-namespace:WPF     5   	     � Title$  TeamStatistics��5   	   $	��200��6    $	��400��6-    � WindowStartupLocation$ CenterScreen=�69    � Triggers 5	      :� 5
   
    :� RoutedEvent$ Loaded��6   .[�5      �� .��5      �� .��5      W�  �� TargetProperty$	 Top�6&    W� From$
 -200X�6F   	 W� To$ 0X�6R    g� Duration$ 0:0:0.5D�6Y   5      5      5      .��5      � $��	0,0,0,-91q�6   .+�5   
   �� -   5         ��Name$ lblLName��5      $��16(�6   $
��Left=�60   $��10q�6K   $��Name:��6W   �� 5   
   -   5        $ lblName��5      $��16(�6   $
��Left=�6/   $��
66,10,0,10q�6J   $��Japan��6^   �� 5   
   -   5        $ lblLFifaCode��5      $��16(�6   $
��Left=�64   $��10, 40q�6O   $��
FIFA code:��6_   �� 5   
   -   5        $ lblFifaCode��5      $��16(�6   $
��Left=�63   $��
92,40,0,40q�6N   $	��JPN��6b   �� 5   
   -   5        $ lblLGamesStats��5      $��16(�6   $
��Left=�66   $��10, 70q�6Q   $)��#Number of games/winds/losses/draws:��6a   �� 5   
   -   5        $ lblGamesStats��5      $��16(�6   $
��Left=�65   $��314,70,0,10q�6P   $��1/1/1/1��6e   �� 5   
   -   5        $ lblLGoalsStats��5      $��16(�6   $
��Left=�66   $��10, 100q�6Q   $(��"Goals scored/conceived/difference:��6b   �� 5   
   -   5        $ lblGoalsStats��5      $��16(�6   $
��Left=�65   $��290,100,0,-20q�6P   $��1/1/1��6g   5      5      