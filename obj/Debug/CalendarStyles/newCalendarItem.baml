   M S B A M L   `   `   `  ����W  SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089   CalendarItemStyle1  �� 
TargetType5   /System.Windows.Controls.Primitives.CalendarItem �� Property �� Value �� 
TargetType � 	Resources �Name � 	Resources  Key �� Color'  !System.Windows.VisualStateManager   VisualStateGroups%  System.Windows.VisualStateGroup	   States   System.Windows.VisualState
   
Storyboard g� Duration	 W� To �� TargetProperty �� 
TargetName �� CornerRadius � Cursor	 �� To F�Name �� Stretch ��Name �� TextAlignment � ColumnDefinitions � RowDefinitions ��Name  HeaderButtonTemplate �� Orientation  PreviousButtonTemplate  NextButtonTemplate ��Name  DisabledColor J� Opacity �� RadiusY �� RadiusX �� Triggers P� Property  P� Value! �� 
TargetName" �� Value# �� Binding$ �� RelativeSource% �� AncestorType&   System.Windows.Controls.Calendar�� 5      D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     :x,http://schemas.microsoft.com/winfx/2006/xaml      5      %�  &	        7  7  7  7  7  �� 5      "    6'   .�5   
   ��  � ��$ 0,3,0,3q�6#   �� 5   
    � ��$ 0q�6,   �� 5   
    � ��$
 Left=�67   �� 5   
    � �� 5      �� 5	      "   6"    5
      ���� 5      (��       6'   **(CalendarItem.DayTitleTemplateResourceKey+).R�5      �� ���#fff$
��Bold"�6:   $	��9.5(�6L   $��Trebuchet MS)�6[   $��Center=�6u   $��0,6,0,6q�6�   $��Center=�6�   ��6�   �� 6�   5      5      .R�5      � $ 	PART_Root��6    5      ���� 5      DisabledColor $ 	#A5FFFFFF��6D   5       5        5      CommonStates��.	 5   "     
Normal��6.     5   "   Disabled��.
 5   &   �� .��5   *   W� $ 0D�6:   $ 1X�6G   $ Opacity�6N   $ PART_DisabledVisual��6r   5   '   5   #   5      .+�5      �� #��z��6!   #��z��6M   #��z��6�   $ 20��6�   ._�5      �  5    "   ���� 5!   &   PreviousButtonTemplate " ��6U   .R�5"   *   �� $
 Hand��61   ���#FD413C$ 20��5$   /    5%   .     5&   2   CommonStates��.	 5'   6     
Normal��6B     5(   6   	MouseOver��.
 5)   :   �� .��5*   >   �� $ 0D�6M   $ 	#FF73A9D8X�6Z   $* $(Shape.Fill).(SolidColorBrush.Color)�6i   $
 path��6�   5+   ;   5,   7     5-   6   Disabled��.
 5.   :   �� .��5/   >   W� $ 0D�6N   $ .5X�6[   $" (Shape.Fill).(Brush.Opacity)�6c   $
 path��6�   50   ;   51   7   52   3   ._�54   .   � .+�55   2   F� $
 path��67   O�����d"�SA ��z b,� цp  $� ��z �� ��d"^���T8"���r�  $� �T8�BW 	���#FFF$��Center=�56   C   $��8��6`   $ Uniform=�6k   $��Center=�6}   57   /   58   +   59   '   �� 5:   &   NextButtonTemplate " ��6Q   .R�5;   *   �� $
 Hand��61   ���#FD413C$ 20��5=   1    5>   .     5?   2   CommonStates��.	 5@   6     
Normal��6B     5A   6   	MouseOver��.
 5B   :   �� .��5C   >   �� $ 0D�6M   $ 	#FF73A9D8X�6Z   $* $(Shape.Fill).(SolidColorBrush.Color)�6i   $
 path��6�   5D   ;   5E   7     5F   6   Disabled��.
 5G   :   �� .��5H   >   W� $ 0D�6N   $ .5X�6[   $" (Shape.Fill).(Brush.Opacity)�6c   $
 path��6�   5I   ;   5J   7   5K   3   ._�5M   .   � .+�5N   2   F� $
 path��67   O����.�� �SA ��� �BW ��L $� ��� "��.�� ^����m�� �z9x $� ��mi,� 	���#FFF$��Center=�5O   D   $��8��6a   $ Uniform=�6l   $��Center=�6~   5P   /   5Q   +   5R   '   �� 5S   &   HeaderButtonTemplate " ��6S   .R�5T   *   � $
 Hand��6/    5U   .     5V   2   CommonStates��.	 5W   6     
Normal��6B     5X   6   	MouseOver��.
 5Y   :   �� .��5Z   >   �� $ 0D�6M   $ #606467X�6Z   $6 0(TextElement.Foreground).(SolidColorBrush.Color)�6g   $ buttonContent��6�   5[   ;   5\   7     5]   6   Disabled��.
 5^   :   �� .��5_   >   W� $ 0D�6N   $ .5X�6[   $ Opacity�6c   $ buttonContent��6�   5`   ;   5a   7   5b   3   .+�5d   .   �� $ buttonContent��6?   #��z��5e   ?   #��z��5f   ?   ���#fff#��z��5h   ?   $��8,4,1,9q�6�   #��z��6�   # z��5i   ?   5j   +   5k   '   5l   #    5m   "   �� 5n   &   $
��Auto �67   $��79��5o   7   �� 5p   &   $��* �67   �� 5q   &   $
��Auto �67    5s   "   �� 5t   &   $��40 �64   �� 5u   &   $��* �64   .+�5x   "   �� $ PART_HeaderButton��6)   $��0��6D   $
��Bold"�6T   |�.  $��13(�6x   $��6 0 0 0q�6�   $��Center=�6�   $��0��6�   8��  $��Bottom=�6�   �� 5z   "   $��2��5{   %   $��	0 10 13 0q�5|   %   $ 
Horizontal=�5}   %   $��0��5~   %   .+�5�   &   �� $ PART_PreviousButton��6-   |�.  $��17��6]   $��17��5�   -   8�� 5�   -   �� 5�   &   $ PART_NextButton��6-   |�.  $��17��6X   $��17��5�   -   8�� $��6 0 0 0q�5�   -   5�   #   � 5�   "   $ PART_MonthView��6'   $��3��6?   $��Center=�6S   $��6q�6p   $��1��6{   $y�Visible=�6�    5�   &   �� 5�   *   $��30 �6;   �� 5�   *   $��30 �6;   �� 5�   *   $��30 �6;   �� 5�   *   $��30 �6;   �� 5�   *   $��30 �6;   �� 5�   *   $��30 �6;   �� 5�   *   $��30 �6;    5�   &   �� 5�   *   $
��Auto �68   �� 5�   *   $
��Auto �68   �� 5�   *   $
��Auto �68   �� 5�   *   $
��Auto �68   �� 5�   *   $
��Auto �68   �� 5�   *   $
��Auto �68   �� 5�   *   $
��Auto �68   5�   #   � 5�   "   $ PART_YearView��6'   $��3��6>   $��Center=�6R   $��6q�6o   $��1��6z   $y�Hidden=�6�    5�   &   �� 5�   *   $
��Auto �6;   �� 5�   *   $
��Auto �6;   �� 5�   *   $
��Auto �6;   �� 5�   *   $
��Auto �6;    5�   &   �� 5�   *   $
��Auto �68   �� 5�   *   $
��Auto �68   �� 5�   *   $
��Auto �68   5�   #   5�      5�      �� 5�      $ PART_DisabledVisual��6$   8�� $ 0Q�6g   $ 2��6s   $ 2��6   $
 Fill=�6�   8�� $��1��6�   $y�	Collapsed=�6�   5�       5�      P� 5�       � {�  .  . �5�      �� $! PART_DisabledVisual��6;    � y�$ Visible=�6\   5�      �� 5�      $
" Year��6�   # 6&   �� *DisplayMode+$ �� *FindAncestor+"%  .a�5�      �� $! PART_MonthView��6;    � y�$ Hidden=�6W   �� 5�      $! PART_YearView��6;    � y�$ Visible=�6V   5�      �� 5�      $" Decade��6�   # 6&   �� *DisplayMode+$ �� *FindAncestor+"%  .a�5�      �� $! PART_MonthView��6;    � y�$ Hidden=�6W   �� 5�      $! PART_YearView��6;    � y�$ Visible=�6V   5�      5�      5�      5�      5�      