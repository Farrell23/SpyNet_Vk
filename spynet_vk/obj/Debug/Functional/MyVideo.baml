   M S B A M L   `   `   `  ����D  @SpyNet_Vk, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null%clr-namespace:SpyNet_Vk	SpyNet_Vk  5      9!clr-namespace:SpyNet_Vk.ViewModelSpyNet_Vk.ViewModel  5      9!clr-namespace:SpyNet_Vk.ConverterSpyNet_Vk.Converter  5          SpyNet_Vk.MyVideo   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      )localclr-namespace:SpyNet_Vk     5      2view!clr-namespace:SpyNet_Vk.ViewModel     5      5convert!clr-namespace:SpyNet_Vk.Converter     5      ���#212121$	��580��5
   M   $	��560��6Y     � DataContext  5      $  SpyNet_Vk.ViewModel.VideoModel  5   
    � 	Resources 5      .   (SpyNet_Vk.Converter.CountMemberConverter   myViewsConvert��%   &	          5   
   6'   5      .��5      � .+�5   
   �� $5��/Количество видеозаписей: ��6   $��30��64   
�������$��28,29,0,501q�6S   $
��Left=�6h   $	��157��6�   �� 5   
   $��30��6   $��40��6   
�������$��185,29,355,501q�6:   ��6R   �� *Length_video+(� 5   
   $��25��6   $��25��6   $
��Left=�6)   $��
331,29,0,0q�6D   $	��Top=�6X   .��5      � -   5        $��	prog_view��5       � ToolTip$# Идет обновление��6J   y�6$   �� *EnableProgress+.+�5         SpyNet_Vk.ProgressBar  5      5      � 5   
   -   5        $��check��5      $��40��6   $��40��6'   $��
356,19,0,0q�63   $
��Left=�6G   $	��Top=�6b   .+�5      �� -   5         ��Name$ 
btn_Update��5      
������ $��0q�6B   $ Обновить��6V   ��6i   �� *Update_Video+.��5       F� $��22��6   $��26��6"    �� Left$ 13��6.   
 �� Top$ 11��6?    �� Stretch$
 Fill=�6O     FillPath#��[ 6^     refreshStyle0#��[ 6   5!      5"      �� 5$   
   -   5         ��Name$ 
list_video��5$      $
��Left=�6_   $	��462��6z   $��	28,59,0,0q�6�   $	��Top=�6�   $	��368��6�   
���������6&   �� *video+ �� IsAsync . 	 �� Mode$	 TwoWay=���5%      �� 5&      .R�5'      �� 
���   �$��1q�61   $��60��6E   $	��300��6Q   ._�5(      �� $��5q�6%   
 �� Orientation$
 
Horizontal=�60    6I   �� *Title+.+�5)      �� $��50��6@   $��50��6L   $��5q�6W   ��6$   �� *
Photo130+6W   �� 5+      .+�5,   "   �� 
���   �$��2q�6V   ��6,   �� *Title+6V   �� 5-   "   
���   �$��2q�6�   ��6,   �� *
ViewsCount+ �� 	Converter# [   �� StringFormat$ Просмотров: {0}��6�   5.      50      51      52      54      �� 56   
   $��Удалить��6   $
��Left=�6#   $��
414,78,0,0q�6>   $	��Top=�6R   $��30��6j   $	��145��6v   ��6�   �� *Delete_SelectVideo+�� 57   
   $&�� Удалить все видео��6   $
��Left=�6-   $��414,120,0,0q�6H   $	��Top=�6]   $��30��6u   $	��145��6�   ��6�   �� *Delete_AllVideo+59      5:      