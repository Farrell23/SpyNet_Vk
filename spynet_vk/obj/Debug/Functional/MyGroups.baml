   M S B A M L   `   `   `  ����D  @SpyNet_Vk, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null9!clr-namespace:SpyNet_Vk.ViewModelSpyNet_Vk.ViewModel  5      %clr-namespace:SpyNet_Vk	SpyNet_Vk  5      9!clr-namespace:SpyNet_Vk.ConverterSpyNet_Vk.Converter  5          SpyNet_Vk.MyGroups   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      2view!clr-namespace:SpyNet_Vk.ViewModel     5      )localclr-namespace:SpyNet_Vk     5      5convert!clr-namespace:SpyNet_Vk.Converter     5      ���#212121$	��580��5
   #   $	��560��6/     � DataContext  5      %  SpyNet_Vk.ViewModel.GroupsModel  5   
    � 	Resources 5      .   (SpyNet_Vk.Converter.CountMemberConverter   myGroupMemberConverter��%   &	          5   
   6'   5      .��5      � .+�5   
   �� $'��!Количество групп: ��6   $��30��6-   
�������$��28,24,436,506q�6L   �� 5   
   -   5         ��Name$ labelCountGroups��5      $��30��6*   $��40��66   
�������$��140,24,400,506q�6T    J� RenderTransformOrigin$ 
2.15,0.433&�6�   ��6l   �� *Length_groups+ �� Mode$ TwoWay=�6�   �� 5   
   -   5         ��Name$ btn_Delete_All_groups��5      $(��"Удалить все группы��60   $
��Left=�6M   $��
356,54,0,0q�6h   $	��Top=�6|   $��30��6�   $	��160��6�   ��6�   �� *Delete_AllGroups+�� 5   
   -   5        $ btn_delete_SelectGroups��5      $��Удалить��62   $
��Left=�6D   $��356,110,0,0q�6_   $	��Top=�6t   $��30��6�   $	��160��6�   ��6�   �� *Delete_SelectGroups+(� 5   
   $��25��6   $��25��6   $��Center=�6)   $��263,24,292,511q�6F   .��5      � -   5        $��	prog_view��5       � ToolTip$# Идет обновление��6J   y�6$   �� *EnableProgress+.+�5         SpyNet_Vk.ProgressBar  5      5      � 5    
   -   5        $��check��5       $��40��6   $��40��6'   $��288,14,252,506q�63   .+�5!      �� -   5        $ 
btn_Update��5!      
������ $��0q�6B   $ Обновить��6V   ��6i   �� *Update_Groups+.��5"      F� $��22��6   $��26��6"    �� Left$ 13��6.   
 �� Top$ 11��6?   	 �� Stretch$
	 Fill=�6O     FillPath#��[ 6^     refreshStyle0#��[ 6   5#      5$      �� 5&   
   -   5        
 ��Name$
 
listGroups��5&      $��28,54,252,50q�6`   
������� �� SelectionMode$ Multiple=�6�   ��6&   �� *groups+ �� IsAsync . $ TwoWay=���5'      �� 5(      .R�5)      �� 
���   �$��1q�61   $��60��6E   $	��260��6Q   ._�5*      �� $��5q�6%    �� Orientation$ 
Horizontal=�60    6I   �� *Name+.+�5+      �� $��50��6@   $��50��6L   $��5q�6W   ��6$   �� *
Photo100+6W   �� 5-      .+�5.   "   �� $
��Bold"�6B   
���   ���6,   �� *Name+6T   �� 5/   "   
���   ���6,   �� *MembersCount+ �� 	Converter# [   �� StringFormat$ Участников: {0}��6�   �� 50   "   $
��Bold"�6F   
���   ���6,   �� *
Activity+6X   51      53      54      55      57      5:      5;      