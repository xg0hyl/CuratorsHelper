   M S B A M L   `   `   `  ���� I  ECuratorsHelper, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null/clr-namespace:CuratorsHelperCuratorsHelper  /clr-namespace:CuratorsHelperCuratorsHelper       CuratorsHelper.ProfilePage   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008     .localclr-namespace:CuratorsHelper     0helpersclr-namespace:CuratorsHelper       N� Title$  
GroupsPage�� � 	Resources #   CuratorsHelper.ConverterImage   ConverterImage��%   &	          .���  � RowDefinitions �� $	��140 ��� $��60 ��� $��2* ��� .+�� $��0�����#222�� $��50��$	��Top=�$��Right=�����  �� 
TargetType" ��.�-   ��  �� Property � �� �� Value$3 -/CuratorsHelper;component/images/settings.png�� �� Triggers P�  P� Property �@J�IsMouseOver P� Value . . ���  � ��$9 3/CuratorsHelper;component/images/settings_MOUSE.png��A� $��0��$��2��$	��170��$	��170��$��Bottom=�$
��Left=�$��	20 0 0 10q����#E7E7E7$��5������ 	 �� ImageSource	 �� *photo+
 �� 	Converter#
 [  �� $��0��$��56(�
�������$
��Bold"�$��Times New Roman)�$��Bottom=�$��	200 0 0 0q����� *FIO+� $��2��$��
70 70 50 0q� �� �� �� �� �� �� $��3* � � ColumnDefinitions �� $��80 ��� �� $
��0.8* �.+��� $��0��$��0��$/��)/CuratorsHelper;component/images/date.png���� $��0��$��1��$��36(�$��Times New Roman)�$
��Left=�$��Center=����� *	date_born+ �� StringFormat$ 
dd.MM.yyyy���� $��1��$��0��$0��*/CuratorsHelper;component/images/phone.png���� $��1��$��1��$��36(�$��Times New Roman)�$
��Left=�$��Center=����� *phone+�� $��2��$��0��$0��*/CuratorsHelper;component/images/email.png���� $��2��$��1��$��36(�$��Times New Roman)�$
��Left=�$��Center=����� *email+�� $��3��$��0��$4��./CuratorsHelper;component/images/education.png���� $��3��$��1��$��36(�$��Times New Roman)�$
��Left=�$��Center=����� *	education+�� $��4��$��0��$1��+/CuratorsHelper;component/images/groups.png���� $��4��$��1��$��36(�$��Times New Roman)�$
��Left=�$��Center=����� *Groups.name+�� -   $��5��$��0��$	��Top=�$��50��$-��'/CuratorsHelper;component/images/CK.png���� $��5��$��1��$��36(�$��Times New Roman)�$
��Left=�$	��Top=����� *cyclics.name+� -   $��MoveGrid��$��5��$��0��$��2��$	��557��$��0��$	��Top=�$��0 60 0 0q� �� �� ��  �� $��80 ��� .+��� $��0��$��0��$��ФИО��$��20(�$��Center=�$��Center=�$��Times New Roman)��� $��1��$��0��$��Каб.��$��20(�$��Center=�$��Center=�$��Times New Roman)��� $��2��$��0��$��Тел.��$��20(�$��Center=�$��Center=�$��Times New Roman)��� $��1��$��1��$��20(�$��Center=�$
��Left=�$��Times New Roman)����� *cyclics.cabinet+�� $��0��$��1��$��20(�$��Center=�$
��Left=�$��Times New Roman)����� *cyclics.FIO_predsedatel+�� $��2��$��1��$��20(�$��Center=�$
��Left=�$��Times New Roman)����� *cyclics.phone_predsedatel+