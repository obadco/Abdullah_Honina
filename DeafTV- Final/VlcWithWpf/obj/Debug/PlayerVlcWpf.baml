   M S B A M L   `   `   `  ����  
VlcWithWpf    VlcWithWpf.PlayerVlcWpf   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35H 9http://schemas.microsoft.com/winfx/2006/xaml/presentation      5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5        � 	Resources  5       �� Source�� 5
   
   $ GlsBtn.xaml?�6   %    .��5      � -   5         �Name$ grdMain��5      
�������$
��Auto��61   $
��Auto��6>    � RowDefinitions 5   
   �� 5      $��* �6   �� 5      $��70 �6   .+�5   
   �� -   5         ��Name$ mediaPlayerBorder1��5      
�������$��0��6A   $��1,1,1,1q�6N   $��2,-1,5,5q�6h    �� CornerRadius$ 5,5,5,5��6z   ._�5      � .+�5      r� -   5        $��mdElm��5      $
��Auto��6/   $
��Auto��6=   $��-1,0,0,0q�6�    r� Stretch$
 Fill=�6�   6�   �� 5      -   5         ��Name$ lblSub��5      $
��Left=�6-   $��Bottom=�6I   ���	#FFEEEE52 �� TextAlignment$ Justify=�6}   $��15(�6�   $
��Bold"�6�   $
��Wrap=�6�   $	��250��6�   $��	200,0,0,0q�6�   5      5       �� 5"   
   -   5        $ mediaPlayerBorder2��5"      $
��Auto��6/   $
��Auto��6<   $	��320��6J   $	��240��6Z   $	��150��6j   $	��100��6z   
�������$��0��6�   $��1,1,1,1q�6�   $��Stretch=�6�   $��Bottom=�6�   $ 5,5,5,5��6�   $��428,-34,8,10q�6  ._�5#      tOclr-namespace:System.Windows.Forms.Integration;assembly=WindowsFormsIntegration System.Windows.Forms.Integration 6A   7  1System.Windows.Forms.Integration.WindowsFormsHost  6   ^myOclr-namespace:System.Windows.Forms.Integration;assembly=WindowsFormsIntegration     6A   -   5        	  Name$	 windowsFormsHost1��5#   "   6A   5%      �� 5'   
   -   5        $ brdControls��5'      $��2��6&   $��2��64   $ 5,5,5,5��6H   
�������
�������$��4,4,4,4q�6�   ._�5*      �� $
��Auto��6   .+�5+      � $
��Auto��6   .+�5-      �� -   5        
 ��Name$
 btnPlay��5-      $��3��6/   $��38��6X   $��41��6d   $��206,17,194,2q�6p    
  GlsBtn#��[  6�   .��5.      �� $��Play.gif��6    $��28��62   $��27��6=   50      �� 52      -	   5        $
 btnStop��52      $��27��6/   $��42��6<   $��160,23,239,7q�6G   #��[  6]     	TtbtnStop � ToolTip# �  6}   .��53      �� $��13��6$   
���   �$��2��6?   $��12��6S    J� RenderTransformOrigin$ 
0.75,0.154&�6^   ��54      
� 55   "    �� RelativeTransform 58   &   V� 59   *   .~�5:   .   ��  �� CenterX$	 0.5Q�6=    �� CenterY$	 0.5Q�6K    �� ScaleX$	 1.5Q�6Y    �� ScaleY$	 1.5Q�6f   T� 5;   .    T� X$
 0.02Q�6A    T� Y$	 0.3Q�6J   5<   +   5>   #   5@      5A      �� 5D      -
   5        $��	lblPeriod��5D      $��Stretch=�60   $	��Top=�6N   $
��Auto��6f   $
��Auto��6s   
���   �$
��10pt(�6�   $��
0,-6,408,0q�6�   �� 5E      -   5        $
 btnPause��5E      $��27��6H   $��34��6U   $��253,23,154,7q�6`   #��[  6v     
TtbtnPause# �  6�   .��5F      �� .+�5G      �� 
���   �$��2��67   $��2��6K   $��12��6U    �� Left$ -3��6a   
 �� Top$ -7��6r   �� 5H      
���   �$��2��67   $��2��6K   $��12��6U   $ 3��6a   $ -7��6q   $ 0.5,0.5&�6�   z�5I   "   V� 5J   &   .~�5K   *   �� $ -1Q�69   �� 5L   *   �� 5M   *   T� 5N   *   5O   '   5Q      5R      5U      �� 5V      -   5         ��Name$ volumeSlider��5V       � Minimum$ 0Q�6X    � Maximum$ 1Q�6d   $	��105��6p   $��Right=�6}   $��	0,26,12,9q�6�     TtvolumeSlider# �  6�     SeekSliderStyle#���  6�    �� IsMoveToPointEnabled . 6  �� 5X      -   5        $��timelineSlider��5X      $��59,-5,61,40q�6[   $��Stretch=�5Y   8    .  �� TickFrequency$	 120Q�6r     5Z      �� 5[      $ SliderBar.xaml?�61   %    5`      �� 5a      -   5         ��Name$ lblTotal��5a      $��Right=�61   $	��Top=�6M   $
��Auto��6e   $
��Auto��6r   
���   �$
��10pt(�6�   $��0,-5,0,0q�6�   5b      5c      5e      5i      5j      