   M S B A M L   `   `   `  ���� G  CMusic Player, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null7 clr-namespace:Music_Player.ViewsMusic_Player.Views  R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089c _System.Windows.Interactivity, Version=4.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35   CircularButton  �� 
TargetType �� Property �� Value �� 
TargetType A�Name J� Effect3   -System.Windows.Media.Effects.DropShadowEffect    
BlurRadius    ShadowDepth �� Triggers	 P� Property
 P� Value �� 
TargetName  CircularButtonNoShadow  
TextButton  CapsuleButton �� CornerRadius  SongListBoxItem �� Padding J� SnapsToDevicePixels �� ContentStringFormat �� ContentStringFormat ]� 
Conditions �� Property �� Value  TabItemIconTemplate  TabItemTemplate �� TextAlignment �� Padding � LayoutTransform �� Angle  	PlayImage �� Triggers �� Binding �� UpdateSourceTrigger �� Value  
PauseImage 	 SliderRepeatButton 
 SliderRepeatButton1  SliderThumb 
 Slider � RowDefinitions ]�Name ]� DecreaseRepeatButton ]� Thumb.  (System.Windows.Interactivity.Interaction    Triggers/  )System.Windows.Interactivity.EventTrigger!   	EventName.  (System.Windows.Interactivity.TriggerBase"   Actions6  0System.Windows.Interactivity.InvokeCommandAction#   Command$ ]� IncreaseRepeatButton  
SongSlider�� D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    :x,http://schemas.microsoft.com/winfx/2006/xaml     *local clr-namespace:Music_Player.Views  Bi:http://schemas.microsoft.com/expression/2010/interactivity  %  &	        &	 T    &	 �    &	 �    &	 |    &	 �    &	 ,    &	 �	    &	 $
    &		 �
    &	
 �    &	 2    &	 .    7 
 7  7 	 &	 �    7  �� "  ��.���  � ��
 ���� ��  � ��$ 0q���  � �� �� " ��.R�� .+�A� $ ButtonBorder��
������     $ 10Q�$ 0Q��� $��Center=�$��Center=� P� 	 �@J�IsMouseOver
 . . ��� $ ButtonBorder�� � �� �#474747P� 	 � ��
 . . ��� $ ButtonBorder�� � ��
 ������� "  ��.���  � ��
 ���� ��  � ��$ 0q���  � �� �� " ��.R�� .+�A� $ ButtonBorder��
������ �� $��Center=�$��Center=� P� 	 �@J�IsMouseOver
 . . ��� $ ButtonBorder�� � ��
 �   �P� 	 � ��
 . . ��� $ ButtonBorder�� � ��
 ������� "  ��.���  � ��
 ���� ��  � ��$ 0q���  � ��
 �������  � �� �� " ��.R��� $��Center=�$��Center=� P� 	 �@J�IsMouseOver
 . . ���  � ��
 �����P� 	 � ��
 . . ���  � ��
 �iii��� "  ��.���  � ��
 ���� ��  � ��$ 0q���  � ��$ DemiBold"���  � ��$ 15(���  � �� �� " ��.R�� .+��� $��Capsule��$��1q�$ 20,20,20,20��
������ 
�������    $ 10Q�$ 0Q��� $��Center=�$��Center=� P� 	 �@J�IsMouseOver
 . . ��� $ Capsule�� � ��
 �����P� 	 � ��
 . . ��� $ Capsule�� � �� �#474747P� 	 � {�
 .  . ��� $ Capsule�� � ��
 �������  � �� �#292929�� "  ��.���  � �� �� " ��.R��� $��Border��#��z��# z��#��z��#��z�� . ._��� #��z��#��z��# z #��z��#��z��# z  ]�  ��  �@��IsSelectionActive . ��  �@��
IsSelected . P� 	 �@J�IsMouseOver
 . . ��� $ Border�� � �� �#4a4a4a�� $ Border�� � ��
 ������� " ��.R�� .+�A� $��ButtonBorder��
������ $��40��$��40��    $ 10Q�$ 0Q��� $2��,/Music Player;component/Images/WhitePlay.png��$��25��$��25��$��4 0 0 0q� P� 	 �@J�IsMouseOver
 . . ��� $ ButtonBorder�� � ��
 �iii�P� 	 �@��
IsSelected
 . . ��� $ ButtonBorder�� � ��
 �OO/��� " ��.R�� .+��� $��TabText��#��z��$ Center=�$��Center=�$��18(�
�������$��50��$ 0 10 0 0q�$
��0 10q� �� $ 90Q� P� 	 �@J�IsMouseOver
 . . ��� $ TabText�� � ��
 �����P� 	 �@��
IsSelected
 . . ��� $ TabText�� � ��
 ������� $ TabText�� � ��$
 Bold"��� $ TabText�� � ��$ 20(��� "  ��.���  � y�$ Visible=� ��  �� *	IsPlaying+$ PropertyChanged=� True.a���  � y�$ 	Collapsed=��� "  ��.���  � y�$ 	Collapsed=� ��  �� *	IsPlaying+$ PropertyChanged=� True.a���  � y�$ Visible=��� "  ��.���  �@J�SnapsToDevicePixels . ��  �@�OverridesDefaultStyle . ��  � �� .  ��  � |� .  ��  � �� �� " ��.R��� $��1q����#303030���#303030$��3���� "  ��.���  �@J�SnapsToDevicePixels . ��  �@�OverridesDefaultStyle . ��  � �� �� " ��.R���  . 
�������$��1q�
�������$��3���� "  n�.���  �@J�SnapsToDevicePixels . ��  �@�OverridesDefaultStyle . ��  � �� �� " n�.R�A� $��10��$��10������ "  A�.���  � ��
 ����� P� 	 �@J�IsMouseOver
 . . ���  � ��
 ������� " ��.R��  �� $
��Auto ��� $
��Auto �#��z���� $
��Auto �.+�]� $ 
PART_Track��$��1�� �� 8��  $��Slider.DecreaseLarge�� n� 8��     $! DragStarted��."   # �� *SliderDragStartedCommand+  $! DragCompleted��."   # �� *SliderDragCompletedCommand+$ �� 8�� $��Slider.IncreaseLarge���� "  ��.���  � |� .  ��  �@J�SnapsToDevicePixels . ��  �@�OverridesDefaultStyle .  P� 	 �@��Orientation$
 
Horizontal=�. ���  � ��$ 21����  � ��$	 104����  � ��8   