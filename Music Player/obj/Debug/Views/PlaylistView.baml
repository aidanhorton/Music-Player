   M S B A M L   `   `   `  ����G  CMusic Player, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null7 clr-namespace:Music_Player.ViewsMusic_Player.Views  5      A%clr-namespace:Music_Player.ConvertersMusic_Player.Converters  5      K GMusicPlayer.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullaBclr-namespace:MusicPlayer.Data.BaseTypes;assembly=MusicPlayer.DataMusicPlayer.Data.BaseTypes 5	      %    Music_Player.Views.PlaylistView   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      2local clr-namespace:Music_Player.Views     5      <
converters%clr-namespace:Music_Player.Converters     5      c _System.Windows.Interactivity, Version=4.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35Bi:http://schemas.microsoft.com/expression/2010/interactivity  5      R	baseTypesBclr-namespace:MusicPlayer.Data.BaseTypes;assembly=MusicPlayer.Data  5	        � 	Resources  5       �� MergedDictionaries �� Source:   4Music_Player.Converters.BooleanToVisibilityConverter   Key�� 5   
    5      �� 5      $ ResourceDictionary.xaml?�6%   �� 5        5       BooleanToVisibilityConverter  6>   5      %    5      .��5      �  � RowDefinitions 5   
   �� 5      $��50 �6   �� 5      .+�5   
   �� $��2��6   .+�5       �� $��	Playlists��6   $��20(�6)   $
��Bold"�67   
�������$��Center=�6\   $��0 10 0 5q�6y   �� 5"      $��10q�6   
�������$��	50 0 50 0q�6@   5#      � 5%   
   $��1��6   y�6   �� *AddingSongToPlaylist+   BooleanToVisibilityConverter �� 	Converter# [  �� ConverterParameter$ Inverted�� �� FallbackValue$ Visible�� � ColumnDefinitions 5&      �� 5'      �� 5(      .+�5*      ]� $��0��6   .+�5+      �� $	��Top=�6    �� Orientation$ 
Horizontal=�62   $��60 0 0 0q�6K   .+�5,      �� $��	Playlists��6    
�������$��14(�6H   $��DemiBold"�6V   $	��Top=�6l   $��0 1 5 0q�6�   �� 5.        CircularButtonNoShadow#��[ 6G   	 � ToolTip$	 New Playlist��6w   $
��Left=�6�   $��22��6�   $��22��6�   ��6   �� *CreatePlaylistCommand+.��5/      �� $2��,/Music Player;component/Images/WhitePlus.jpg��6    $��20��6V   $��20��6a   50      51      �� 53      -   5        $��PlaylistListBox��53      ���#262626$��Disabled=�6q   $��
50 10 0 10q�6�   $	��Top=�6�   ��61   �� *PlaylistCollection+��54      �� 55      
 �� 
TargetType"
 ��6      SongListBoxItem �� BasedOn# [ 69   .�56      ��  �� Property � �� �� Value 57   "   �� 58   &   .R�59   *   ]� ��.  .+�5:   .   �� $��PlayingIcon��64   $5��//Music Player;component/Images/WhiteSpeaker.png��6G   $y�	Collapsed=�6�    J� IsHitTestVisible .  $��15��6�   $��15��6�   $
��Left=�6�   $��12.5 0q�6�   �� 5<   .   $��
PlayButton��65   $	 Play playlist��6�   #��[ 6�   $y�Hidden=�6#  $��20��67  $��20��6B  $
��10 0q�6N  $
��Left=�6\  ��6G   �� *!DataContext.PlayPlaylistCommand+ �� RelativeSource ��  �� AncestorType" >���6�   �� .��5=   2   �� $2��,/Music Player;component/Images/WhitePlay.png��68   $��15��6n   $��15��6y   $��3 0 0 0q�6�   5>   /   �� 5@   .   $��WordEllipsis=�6N   
�������$��15(�6}   $
��Left=�6�   ��68   �� *Name+6�   5A   +    �� Triggers 5B   *   �� 5C   .    �� Value$
 True��6X    �� Binding 6:   �� *	IsPlaying+.a�5D   2   ��  �� 
TargetName$ PlayingIcon��69    � y�$ Visible=�6h   �� 5E   2   $ 
PlayButton��69    � y�$ 	Collapsed=�6g   5F   /   �� 5G   .   $
 True��6�    6:   �� *IsMouseOver+ �� $ ListBoxItemL�.a�5H   2   �� $ 
PlayButton��69    � y�$ Visible=�6g   �� 5I   2   $ PlayingIcon��69    � y�$ 	Collapsed=�6h   5J   /   5L   '   5N       �� Triggers 5O      P� 5P   "    P� Property �@��
IsSelected P� Value . . �5Q   &   ��  � �� �#6969696C   �� 5R   &    � ��
 �����6D   5S   #   5U      .  (System.Windows.Interactivity.Interaction   Triggers 5W      /  )System.Windows.Interactivity.EventTrigger  5X         	EventName$ SelectionChanged��6)   .  (System.Windows.Interactivity.TriggerBase   Actions. 5Y      6  0System.Windows.Interactivity.InvokeCommandAction     Command 64   �� * SelectedPlaylistChangedCommand+   CommandParameter 6g   �� *SelectedItem+ �� ElementName$ PlaylistListBox��5Z      5\      5]      ]� 5_      $��1��6   .+�5`      � $	��Top=�6   $��	10 2 50 0q�6,   .+�5a      �� $��Songs��6    
�������$��14(�6D   $��DemiBold"�6R   �� 5c      $��Right=�6x   
���iii�
�������$��Center=�6�   $	��100��6�   $��0 0 20 2q�6�   ��6   �� *SongSearchText+ �� Mode$ OneWayToSource=� �� UpdateSourceTrigger$ PropertyChanged=�6�   �� 5d      $4��./Music Player;component/Images/WhiteSearch.png��6    .  $��Right=�6m   $��15��6�   $��15��6�   $��0 0 2 0q�6�   5e      �� 5h      -   5        $��SongsListBox��5h      ���#262626$��Disabled=�6y   $��
0 10 50 10q�6�   $	��Top=�6�   ��6.   �� *CurrentPlaylistSongCollection+��5i      �� 5j      "
 ��6    # [ 69   .�5k      ��  � ��$ Stretch=�6K   �� 5l       � �� 5m   "   �� 5n   &   .R�5o   *   ]� ��.  .+�5p   .   �� $��PlayingIcon��64   $5��//Music Player;component/Images/WhiteSpeaker.png��6G   $y�	Collapsed=�6�    .  $��15��6�   $��15��6�   $
��Left=�6�   $��12.5 0q�6�   �� 5r   .   $��
PlayButton��65   $	 Play playlist��6�   #��[ 6�   $y�Hidden=�6  $��20��63  $��20��6>  $
��10 0q�6J  $
��Left=�6X  ��6G   �� *DataContext.PlaySongCommand+ �� " >���6�   �� .��5s   2   �� $2��,/Music Player;component/Images/WhitePlay.png��68   $��15��6n   $��15��6y   $��3 0 0 0q�6�   5t   /   �� 5v   .   $��WordEllipsis=�6N   
�������$��15(�6}   $
��Left=�6�   ��68   �� *Name+6�   �� 5x   .   $��AddToPlaylistButton��65   $	 Add to playlist��6�   #��[ 6  $y�Hidden=�68  $��20��6L  $��20��6W  $��Right=�6c  ��6P   ��  �� " >�  �� Path$*  $DataContext.OpenAddToPlaylistCommand���6�   �� .��5y   2   �� $2��,/Music Player;component/Images/WhitePlus.jpg��68   $��15��6n   $��15��6y   5z   /   5{   +    5|   *   �� 5}   .   $
 True��6X    6:   �� *	IsPlaying+.a�5~   2   �� $ PlayingIcon��69    � y�$ Visible=�6h   �� 5   2   $ 
PlayButton��69    � y�$ 	Collapsed=�6g   5�   /   �� 5�   .   $
 True��6�    6:   �� *IsMouseOver+ �� $ ListBoxItemL�.a�5�   2   �� $ AddToPlaylistButton��69    � y�$ Visible=�6p   �� 5�   2   $ 
PlayButton��69    � y�$ Visible=�6g   �� 5�   2   $ PlayingIcon��69    � y�$ 	Collapsed=�6h   5�   /   5�   '   5�      5�      5�      5�      5�      �� 5�   
   $��1��6�   $��0��6�   y�6   �� *AddingSongToPlaylist+# [ $ Hidden��.+�5�      �  5�      �� 5�      $��25 �6$   �� 5�      $��* �6$   �� 5�      $	��120 �6$   .+�5�      � $	��Top=�6   .+�5�      �� $��Add song to playlist:��6    
�������$��15(�6P   $��DemiBold"�6^   $	��Top=�6t   $��50 3 0 0q�6�   �� 5�      $��Right=�6c   
�������$��15(�6�   $��DemiBold"�6�   $��0 3 50 0q�6�   ��6    �� *AddToPlaylistSong.Name+! �� StringFormat$! 
Song - {0}��6�   5�      �� 5�      -   5        $��AddToPlaylistListBox��5�      ���#262626$��Disabled=�6v   $��50 10 50 10q�6�   $��1��6�   ��66   �� *PlaylistCollection+��5�      �� 5�      "
 ��6    # [ 69   .�5�      ��  � �� 5�   "   �� 5�   &   .R�5�   *   ]� ��.  .+�5�   .   �� $��WordEllipsis=�6N   
�������$��15(�6}   $
��Left=�6�   ��68   �� *Name+6�   5�   +   5�   '   5�       5�      P� 5�   "    �@��
IsSelected . . �5�   &   ��  � �� �#6969696C   �� 5�   &    � ��
 �����6D   5�   #   5�       5�        5�      $ SelectionChanged��6)   . 5�         64   �� * SelectedPlaylistChangedCommand+ 6g   �� *SelectedItem+$ AddToPlaylistListBox��5�      5�      � 5�      $��2��6   .+�5�      �� $��Add to selected playlist��6�   $	��Top=�6�   $��40��6�   $	��200��6  $��0 10 0 0q�6  ��6   �� *AddToPlaylistCommand+��6F   �� *SelectedItem+$ AddToPlaylistListBox��{�6�   �� *CanAddToSelectedPlaylist+��5�      �� 5�      "
 ��6$   .�5�   "   ��  � ��
 ���� 6?   �� 5�   "    � ��$ 0q�6D   �� 5�   "    � ��$ DemiBold"�6?   �� 5�   "    � ��$ 15(�6=   �� 5�   "    � �� 5�   &   �� 5�   *   " �� 
TargetType"" ��6:   .R�5�   .   � .+�5�   2   �� $��Capsule��69   $��1q�6H   # �� CornerRadius$# 20,20,20,20��6\   
������ 
�������$ J� Effect$ 5�   6   3  -System.Windows.Media.Effects.DropShadowEffect  5�   :   %   
BlurRadius$% 10Q�6K   &   ShadowDepth$& 0Q�6[   5�   3   �� 5�   2   $��Center=�6C   $��Center=�6`   5�   /   ' �� Triggers' 5�   .   P� 5�   2    �@J�IsMouseOver . . �5�   6   �� $ Capsule��6=    � ��
 �����6h   5�   3   P� 5�   2    � �� . . �5�   6   �� $ Capsule��6=    � �� �#4747476h   5�   3   P� 5�   2    � {� .  . �5�   6   �� $ Capsule��6=    � ��
 �����6h   �� 5�   6    � �� �#2929296S   5�   3   5�   +   5�   #   5�      5�      �� 5�      $��Cancel��6L     
TextButton#��[ 6]   $��15(�6�   $��0 25 0 0q�6�   ��6   �� *CancelAddToPlaylistCommand+6�   5�      5�      5�      5�      5�      