   M S B A M L   `   `   `  ����I  ELagoonLearning, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null/clr-namespace:LagoonLearningLagoonLearning  5   	       LagoonLearning.MainWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   .localclr-namespace:LagoonLearning     5   	     � Title$  
MainWindow��5   	   $	��539��6   $	��363��6)    � 
ResizeMode$ NoResize=�65    � WindowStyle$
 None=�6K    � SizeToContent$ Manual=�6^    � WindowStartupLocation$ CenterScreen=�6u   .��5	      �� ��5
   
   �� 5       �� EndPoint$ 0.5,1&�6"    �� 
StartPoint$ 0.5,0&�63   .��5      �  � Color$ #2783B2��6   � 5      $ #FEE6A6��6    � Offset$ 1Q�6/   5      .+�5   
   o� $��20��6   $	��363��6   	 J� OpacityMask	 �	#FF3737BA���	#FF141C3F���	#FFC6E4F5$��Center=�6m   $��Center=�6�   ��5      �� 5      .R�5      ]� $��Stretch=�6    6@   5      .@�5      m� -   5        $
��Exit��5      $��MV Boli)�6%   $
��Bold"�6:   $��Right=�6d   5      5      � 5   
   $	��539��6   $	��363��6   $��Center=�6(   $	��Top=�6E   .+�5      � $��0,47,0,0q�6   $��Center=�6%   $	��293��6B   $	��445��6N   $	��Top=�6[   .+�5      �� $��1��6   $��1q�6&   ���	#70FFFFFF
 �� CornerRadius$
 25��6Q   $��
0,10,0,-10q�6c   �� 5      -   5         ��Name$ 	LogoImage��5      $��/Images/modified.png��6+   $��UniformToFill=�6I   $��
-7,-7,-7,2q�6a   �� 5      -   5         ��Name$ lblRegNumber��5      $��Username��6.   $��MV Boli)�6A   $
��Bold"�6V   $��115,10,113,204q�6h   $��1��6�   �� 5      -   5         ��Name$ 
txtUserLog��5       �� TextWrapping$
 Wrap=�6.   $	��152��6B   $��27��6N   $
��Left=�6Z   $	��Top=�6u   $��MV Boli)�6�   $��14(�6�   ���	#FF070807$��Normal"�6�   $��	73,40,0,0q�6�   $��1��6�   4  .System.Windows.Automation.AutomationProperties   IsRequiredForForm . 6�   �� 5       -   5        $ lblPersonalAccessCode��5       $��Password��67   $��Center=�6J   $	��Top=�6g   $��MV Boli)�6   $
��Bold"�6�    J� RenderTransformOrigin$ 0.842,1.737&�6�   $��0,72,0,0q�6�   $��1��6�   $��29��6�   $��63��6�   G� 5!      -   5         G�Name$ 
txtPassLog��5!      $	��152��62   $��30��6>   $��14(�6J   $
��Left=�6X   $	��Top=�6s   $��
73,101,0,0q�6�   $��1��6�    . 6�   �� 5"      -   5         ��Name$ btnLogin��5"      $��Log in��6+   $
��Left=�6<   $	��Top=�6W   $	��152��6o   $��MV Boli)�6{   $
��Bold"�6�    � UseLayoutRounding .  $��Center=�6�   $��Disabled=�6�   $��Center=�6  ���	#FF050404���	#FFDECC8F$��30��6}  $��
73,162,0,0q�6�  $��1��6�  �� 5#      -   5        $ btnRegister��5#      $��Register��6.   $	��152��6A   $
��Left=�6M   $	��Top=�6h   $��MV Boli)�6�   $
��Bold"�6�   ���	#FFDECC8F$��30��6�   $��
73,204,0,0q�6�   $��1��6�    � RowDefinitions 5$      �� 5%      $
��201* �6$   �� 5&      $
��244* �6$   5(      5)      5*      5+      