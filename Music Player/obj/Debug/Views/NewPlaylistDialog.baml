   M S B A M L   `   `   `  ����G  CMusic Player, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null7 clr-namespace:Music_Player.ViewsMusic_Player.Views  5   	   *    $Music_Player.Views.NewPlaylistDialog   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   2local clr-namespace:Music_Player.Views     5   	   -   5        ���#1e2838  � Title$  Create new playlist��5	   	    � SizeToContent$ WidthAndHeight=�5
   	    � WindowStartupLocation$ CenterOwner=�5   	   .��5      � $��15q�6    � ColumnDefinitions 5   
   �� 5      $
��Auto �6   �� 5      $��* �6    � RowDefinitions 5   
   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   .+�5   
   �� $2��,/Music Player;component/Images/WhitePlus.jpg��6   $��32��6F   $��32��6Q   $��2��6]   $
��20,0q�6n   �� 5   
   $��Playlist name:��6   $��15(�6)   
�������$��1��6J   $��0��6Z   �� 5   
   -   5         ��Name$ 	AnswerBox��5      
�������$��1��6<   $��1��6L   $	��250��6Y   � 5   
   $��Right=�6   $��0,15,0,0q�60   $��2��6B   $��0��6V   $��2��6f   .+�5      �� -   5        $��Ok��5       �� 	IsDefault . $��60��6E   $��0,0,10,0q�6S   �� 5       $��Cancel��6    �� IsCancel . $��60��66   5!      5"      5#      