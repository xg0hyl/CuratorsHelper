   M S B A M L   `   `   `  ���� W  SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089   CalendarItemStyle1  �� 
TargetType5   /System.Windows.Controls.Primitives.CalendarItem �� Property �� Value �� 
TargetType � 	Resources �Name � 	Resources  Key �� Color'  !System.Windows.VisualStateManager   VisualStateGroups%  System.Windows.VisualStateGroup	   States   System.Windows.VisualState
   
Storyboard g� Duration	 W� To �� TargetProperty �� 
TargetName �� CornerRadius � Cursor	 �� To F�Name �� Stretch ��Name �� TextAlignment � ColumnDefinitions � RowDefinitions ��Name  HeaderButtonTemplate �� Orientation  PreviousButtonTemplate  NextButtonTemplate ��Name  DisabledColor J� Opacity �� RadiusY �� RadiusX �� Triggers P� Property  P� Value! �� 
TargetName" �� Value# �� Binding$ �� RelativeSource% �� AncestorType&   System.Windows.Controls.Calendar�� D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     :x,http://schemas.microsoft.com/winfx/2006/xaml      %D  &	        7  7  7  7  7  �� "    .���  � ��$ 0,3,0,3q���  � ��$ 0q���  � ��$
 Left=���  � �� �� "    ���� (��       **(CalendarItem.DayTitleTemplateResourceKey+).R��� ���#fff$
��Bold"�$	��9.5(�$��Trebuchet MS)�$��Center=�$��0,6,0,6q�$��Center=����� .R�� $ 	PART_Root�� ���� DisabledColor $ 	#A5FFFFFF��   CommonStates��.	   
Normal��  Disabled��.
 �� .��W� $ 0D�$ 1X�$ Opacity�$ PART_DisabledVisual��.+��� #��z��#��z��#��z��$ 20��._��  ���� PreviousButtonTemplate " ��.R��� $
 Hand�����#FD413C$ 20��   CommonStates��.	   
Normal��  	MouseOver��.
 �� .���� $ 0D�$ 	#FF73A9D8X�$* $(Shape.Fill).(SolidColorBrush.Color)�$
 path��  Disabled��.
 �� .��W� $ 0D�$ .5X�$" (Shape.Fill).(Brush.Opacity)�$
 path��._�� .+�F� $
 path��O�����d"�SA ��z b,� цp  $� ��z �� ��d"^���T8"���r�  $� �T8�BW 	���#FFF$��Center=�$��8��$ Uniform=�$��Center=��� NextButtonTemplate " ��.R��� $
 Hand�����#FD413C$ 20��   CommonStates��.	   
Normal��  	MouseOver��.
 �� .���� $ 0D�$ 	#FF73A9D8X�$* $(Shape.Fill).(SolidColorBrush.Color)�$
 path��  Disabled��.
 �� .��W� $ 0D�$ .5X�$" (Shape.Fill).(Brush.Opacity)�$
 path��._�� .+�F� $
 path��O����.�� �SA ��� �BW ��L $� ��� "��.�� ^����m�� �z9x $� ��mi,� 	���#FFF$��Center=�$��8��$ Uniform=�$��Center=��� HeaderButtonTemplate " ��.R�� $
 Hand��   CommonStates��.	   
Normal��  	MouseOver��.
 �� .���� $ 0D�$ #606467X�$6 0(TextElement.Foreground).(SolidColorBrush.Color)�$ buttonContent��  Disabled��.
 �� .��W� $ 0D�$ .5X�$ Opacity�$ buttonContent��.+��� $ buttonContent��#��z��#��z�����#fff#��z��$��8,4,1,9q�#��z��# z�� �� $
��Auto �$��79���� $��* ��� $
��Auto � �� $��40 ��� $��* �.+��� $ PART_HeaderButton��$��0��$
��Bold"�|�.  $��13(�$��6 0 0 0q�$��Center=�$��0��8��  $��Bottom=��� $��2��$��	0 10 13 0q�$ 
Horizontal=�$��0��.+��� $ PART_PreviousButton��|�.  $��17��$��17��8�� �� $ PART_NextButton��|�.  $��17��$��17��8�� $��6 0 0 0q�� $ PART_MonthView��$��3��$��Center=�$��6q�$��1��$y�Visible=� �� $��30 ��� $��30 ��� $��30 ��� $��30 ��� $��30 ��� $��30 ��� $��30 � �� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto �� $ PART_YearView��$��3��$��Center=�$��6q�$��1��$y�Hidden=� �� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto � �� $
��Auto ��� $
��Auto ��� $
��Auto ��� $ PART_DisabledVisual��8�� $ 0Q�$ 2��$ 2��$
 Fill=�8�� $��1��$y�	Collapsed=� P�  � {�  .  . ��� $! PART_DisabledVisual�� � y�$ Visible=��� $
" Year��# �� *DisplayMode+$ �� *FindAncestor+"%  .a��� $! PART_MonthView�� � y�$ Hidden=��� $! PART_YearView�� � y�$ Visible=��� $" Decade��# �� *DisplayMode+$ �� *FindAncestor+"%  .a��� $! PART_MonthView�� � y�$ Hidden=��� $! PART_YearView�� � y�$ Visible=�