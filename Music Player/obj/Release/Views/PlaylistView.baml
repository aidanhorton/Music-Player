   M S B A M L   `   `   `  ���� G  CMusic Player, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null7 clr-namespace:Music_Player.ViewsMusic_Player.Views  A%clr-namespace:Music_Player.ConvertersMusic_Player.Converters  K GMusicPlayer.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullaBclr-namespace:MusicPlayer.Data.BaseTypes;assembly=MusicPlayer.DataMusicPlayer.Data.BaseTypes %    Music_Player.Views.PlaylistView   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008     2local clr-namespace:Music_Player.Views     <
converters%clr-namespace:Music_Player.Converters     c _System.Windows.Interactivity, Version=4.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35Bi:http://schemas.microsoft.com/expression/2010/interactivity  R	baseTypesBclr-namespace:MusicPlayer.Data.BaseTypes;assembly=MusicPlayer.Data    � 	Resources   �� MergedDictionaries �� Source:   4Music_Player.Converters.BooleanToVisibilityConverter   Key��  �� $ ResourceDictionary.xaml?���    BooleanToVisibilityConverter  %    .���  � RowDefinitions �� $��50 ��� .+��� $��2��.+��� $��	Playlists��$��20(�$
��Bold"�
�������$��Center=�$��0 10 0 5q��� $��10q�
�������$��	50 0 50 0q�� $��1��y��� *AddingSongToPlaylist+   BooleanToVisibilityConverter �� 	Converter# [  �� ConverterParameter$ Inverted�� �� FallbackValue$ Visible�� � ColumnDefinitions �� �� .+�]� $��0��.+��� $	��Top=� �� Orientation$ 
Horizontal=�$��60 0 0 0q�.+��� $��	Playlists��
�������$��14(�$��DemiBold"�$	��Top=�$��0 1 5 0q���   CircularButtonNoShadow#��[ 	 � ToolTip$	 New Playlist��$
��Left=�$��22��$��22������ *CreatePlaylistCommand+.���� $2��,/Music Player;component/Images/WhitePlus.jpg��$��20��$��20���� -   $��PlaylistListBox�����#262626$��Disabled=�$��
50 10 0 10q�$	��Top=����� *PlaylistCollection+���� 
 �� 
TargetType"
 ��  SongListBoxItem �� BasedOn# [ .���  �� Property � �� �� Value �� .R�]� ��.  .+��� $��PlayingIcon��$5��//Music Player;component/Images/WhiteSpeaker.png��$y�	Collapsed=� J� IsHitTestVisible .  $��15��$��15��$
��Left=�$��12.5 0q��� $��
PlayButton��$	 Play playlist��#��[ $y�Hidden=�$��20��$��20��$
��10 0q�$
��Left=����� *!DataContext.PlayPlaylistCommand+ �� RelativeSource ��  �� AncestorType" >����� .���� $2��,/Music Player;component/Images/WhitePlay.png��$��15��$��15��$��3 0 0 0q��� $��WordEllipsis=�
�������$��15(�$
��Left=����� *Name+ �� Triggers ��  �� Value$
 True�� �� Binding �� *	IsPlaying+.a���  �� 
TargetName$ PlayingIcon�� � y�$ Visible=��� $ 
PlayButton�� � y�$ 	Collapsed=��� $
 True�� �� *IsMouseOver+ �� $ ListBoxItemL�.a��� $ 
PlayButton�� � y�$ Visible=��� $ PlayingIcon�� � y�$ 	Collapsed=� �� Triggers P�  P� Property �@��
IsSelected P� Value . . ���  � �� �#696969��  � ��
 �����.  (System.Windows.Interactivity.Interaction   Triggers /  )System.Windows.Interactivity.EventTrigger     	EventName$ SelectionChanged��.  (System.Windows.Interactivity.TriggerBase   Actions. 6  0System.Windows.Interactivity.InvokeCommandAction     Command �� * SelectedPlaylistChangedCommand+   CommandParameter �� *SelectedItem+ �� ElementName$ PlaylistListBox��]� $��1��.+�� $	��Top=�$��	10 2 50 0q�.+��� $��Songs��
�������$��14(�$��DemiBold"��� $��Right=�
���iii�
�������$��Center=�$	��100��$��0 0 20 2q����� *SongSearchText+ �� Mode$ OneWayToSource=� �� UpdateSourceTrigger$ PropertyChanged=��� $4��./Music Player;component/Images/WhiteSearch.png�� .  $��Right=�$��15��$��15��$��0 0 2 0q��� -   $��SongsListBox�����#262626$��Disabled=�$��
0 10 50 10q�$	��Top=����� *CurrentPlaylistSongCollection+���� "
 ��# [ .���  � ��$ Stretch=���  � �� �� .R�]� ��.  .+��� $��PlayingIcon��$5��//Music Player;component/Images/WhiteSpeaker.png��$y�	Collapsed=� .  $��15��$��15��$
��Left=�$��12.5 0q��� $��
PlayButton��$	 Play playlist��#��[ $y�Hidden=�$��20��$��20��$
��10 0q�$
��Left=����� *DataContext.PlaySongCommand+ �� " >����� .���� $2��,/Music Player;component/Images/WhitePlay.png��$��15��$��15��$��3 0 0 0q��� $��WordEllipsis=�
�������$��15(�$
��Left=����� *Name+�� $��AddToPlaylistButton��$	 Add to playlist��#��[ $y�Hidden=�$��20��$��20��$��Right=�����  �� " >�  �� Path$*  $DataContext.OpenAddToPlaylistCommand����� .���� $2��,/Music Player;component/Images/WhitePlus.jpg��$��15��$��15�� �� $
 True�� �� *	IsPlaying+.a��� $ PlayingIcon�� � y�$ Visible=��� $ 
PlayButton�� � y�$ 	Collapsed=��� $
 True�� �� *IsMouseOver+ �� $ ListBoxItemL�.a��� $ AddToPlaylistButton�� � y�$ Visible=��� $ 
PlayButton�� � y�$ Visible=��� $ PlayingIcon�� � y�$ 	Collapsed=��� $��1��$��0��y��� *AddingSongToPlaylist+# [ $ Hidden��.+��  �� $��25 ��� $��* ��� $	��120 �.+�� $	��Top=�.+��� $��Add song to playlist:��
�������$��15(�$��DemiBold"�$	��Top=�$��50 3 0 0q��� $��Right=�
�������$��15(�$��DemiBold"�$��0 3 50 0q����� *AddToPlaylistSong.Name+! �� StringFormat$! 
Song - {0}���� -   $��AddToPlaylistListBox�����#262626$��Disabled=�$��50 10 50 10q�$��1������ *PlaylistCollection+���� "
 ��# [ .���  � �� �� .R�]� ��.  .+��� $��WordEllipsis=�
�������$��15(�$
��Left=����� *Name+ P�  �@��
IsSelected . . ���  � �� �#696969��  � ��
 �����   $ SelectionChanged��.    �� * SelectedPlaylistChangedCommand+ �� *SelectedItem+$ AddToPlaylistListBox��� $��2��.+��� $��Add to selected playlist��$	��Top=�$��40��$	��200��$��0 10 0 0q����� *AddToPlaylistCommand+���� *SelectedItem+$ AddToPlaylistListBox��{��� *CanAddToSelectedPlaylist+���� "
 ��.���  � ��
 ���� ��  � ��$ 0q���  � ��$ DemiBold"���  � ��$ 15(���  � �� �� " �� 
TargetType"" ��.R�� .+��� $��Capsule��$��1q�# �� CornerRadius$# 20,20,20,20��
������ 
�������$ J� Effect$ 3  -System.Windows.Media.Effects.DropShadowEffect  %   
BlurRadius$% 10Q�&   ShadowDepth$& 0Q��� $��Center=�$��Center=�' �� Triggers' P�  �@J�IsMouseOver . . ��� $ Capsule�� � ��
 �����P�  � �� . . ��� $ Capsule�� � �� �#474747P�  � {� .  . ��� $ Capsule�� � ��
 �������  � �� �#292929�� $��Cancel��  
TextButton#��[ $��15(�$��0 25 0 0q����� *CancelAddToPlaylistCommand+