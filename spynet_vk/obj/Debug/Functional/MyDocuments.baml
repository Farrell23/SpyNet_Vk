   M S B A M L   `   `   `  ����D  @SpyNet_Vk, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null9!clr-namespace:SpyNet_Vk.ViewModelSpyNet_Vk.ViewModel  5      9!clr-namespace:SpyNet_Vk.ConverterSpyNet_Vk.Converter  5      %clr-namespace:SpyNet_Vk	SpyNet_Vk  5          SpyNet_Vk.MyDocuments   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      2view!clr-namespace:SpyNet_Vk.ViewModel     5      5convert!clr-namespace:SpyNet_Vk.Converter     5      )localclr-namespace:SpyNet_Vk     5      ���#212121$
��Auto��5	   #   $
��Auto��60     � DataContext  5      (  "SpyNet_Vk.ViewModel.DocumentsModel  5   
    � 	Resources 5      (   "SpyNet_Vk.Converter.DocSizeConvert   myDocSizeConvert-   'SpyNet_Vk.Converter.DocPictureConverter  myDocPicConverter��%:   &	        &	        5   
   6!     5   
   6&   5      .��5      � .+�5   
   �� $1��+Количество документов: ��6   $��30��62   $��28,24,405,506q�6>   
�������6U   �� 5   
   -   5         ��Name$ labelCountDocs��5      $��30��6(   $��40��64   $��175,24,365,506q�6?   
���������6j   �� *Length_docs+�� 5   
   -   5         ��Name$ listDocs��5      $��28,59,272,66q�6\   
������� �� SelectionMode$ Multiple=�6�   $	��435��6�   $	��270��6�   ��6$   �� *docs+ �� IsAsync .  �� Mode$ TwoWay=���5       �� 5!      .R�5"      �� 
���   �$��1q�61   $��60��6E   $	��230��6Q   ._�5#      �� $��5q�6%    �� Orientation$ 
Horizontal=�60    � ToolTip 6I   �� *Title+.+�5$      �� $��50��6m   $��50��6y   $��5q�6�   ��6$   �� *	Preview+	 �� 	Converter#	 [ 6�   �� 5&      .+�5'   "   �� $
��Bold"�6C   
���   ���6,   �� *Title+6U   �� 5(   "   
���   ���6,   �� *Ext+6B   �� 5)   "   
���   ���6,   �� *Size+#	 [  
 �� StringFormat$
 Size: {0} Mb��6�   5*      5,      5-      5.      50      �� 52   
   -   5         ��Name$ btn_Delete_All_docs��52      $.��(Удалить все документы��6.   $
��Left=�6N   $��
356,54,0,0q�6i   $	��Top=�6}   $��30��6�   $	��166��6�   ��6�   �� *Delete_AllDocs+�� 53   
   -   5        $ btn_delete_SelectDocs��53      $��Удалить��60   $
��Left=�6B   $��356,110,0,0q�6]   $	��Top=�6r   $��30��6�   $	��166��6�   ��6�   �� *Delete_SelectDocs+(� 55   
   $��25��6   $��25��6   $��238,29,317,506q�6)   .��56      � -   5        $��	prog_view��56      $# Идет обновление��6J   y�6$   �� *EnableProgress+.+�57         SpyNet_Vk.ProgressBar  58      59      � 5;   
   -   5        $��check��5;      $��40��6   $��40��6'   $��263,19,277,501q�63   .+�5<      �� -   5        $ 
btn_Update��5<      
������ $��0q�6B   $ Обновить��6V   ��6i   �� *Update_Docs+.��5=      F� $��22��6   $��26��6"    �� Left$ 13��6.   
 �� Top$ 11��6?    �� Stretch$
 Fill=�6O     FillPath#��[ 6^     refreshStyle0#��[ 6   5>      5?      5A      5B      