   M S B A M L   `   `   `  ����G  CMusic Player, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null7 clr-namespace:Music_Player.ViewsMusic_Player.Views  5      "    Music_Player.Views.AlbumView   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      2local clr-namespace:Music_Player.Views     5      .��5	      �   � RowDefinitions  5
   
   �� 5      $��50 �6   �� 5      .+�5   
   �� $��Albums��6   $��20(�6"   $
��Bold"�60   
�������$��Center=�6U   $��10q�6r   $��0��6~   �� 5   
   ���#303030$��15(�6O   
�������$��Disabled=�6p   $	��500��6�   $��10q�6�   $��1��6�   ��6   �� *AlbumCollection+��5      �� 5       �� 
TargetType" ��6   .�5      ��  �� Property � �� �� Value 5      �� 5      .R�5   "   ��  �� Orientation$ 
Horizontal=�6-   .+�5   &   �� ��60   �� *Album+�� 5   &   
���������60   �� *Artist+ �� StringFormat$  - {0}��6_   5   #   5      5      5      5       5!      5"      