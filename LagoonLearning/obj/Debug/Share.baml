   M S B A M L   `   `   `  ����I  ELagoonLearning, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null/clr-namespace:LagoonLearningLagoonLearning  5   	       LagoonLearning.Share   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   .localclr-namespace:LagoonLearning     5   	     � Title$  Share��5   	   $	��539��6   $	��363��6$    � 
ResizeMode$ NoResize=�60    � WindowStyle$
 None=�6F    � SizeToContent$ Manual=�6Y    � WindowStartupLocation$ CenterScreen=�6p   .��5	      �� ��5
   
   �� 5       �� EndPoint$ 0.5,1&�6"    �� 
StartPoint$ 0.5,0&�63   .��5      �  � Color$ #2783B2��6   � 5      $ #FEE6A6��6    � Offset$ 1Q�6/   5      .+�5   
   o� $��20��6   $	��363��6   	 J� OpacityMask	 �	#FF3737BA���	#FF141C3F���	#FFC6E4F5$��Center=�6m   $��Center=�6�   ��5      �� 5      .R�5      ]� $��Stretch=�6    6@   5      .@�5      m� -   5        
 m�Name$

 Home��5      $
��Home��6%   $��MV Boli)�63   $
��Bold"�6H   6s   m� 5      $��
Flashcards��6   $��MV Boli)�6+   $
��Bold"�6@   .@�5      m� -   5        $
 Create��5      $��Create��6+   6V   m� 5      -   5        $
 Study��5      $��Study��6*   6S   m� 5      -   5        $

 Test��5      $
��Test��6)   6P   5      m� 5      $��Other��6   $��MV Boli)�6&   $
��Bold"�6;   .@�5      m� -   5        $
 
Statistics��5      $��
Statistics��6/   6\   m� 5      -   5        $
 Public��5      $��Publications��6+   6Y   m� 5      -   5        $
 ShareB��5      $��Share��6+   {�.  6N   5       m� 5!      -   5        $
��Exit��5!      $��MV Boli)�6%   $
��Bold"�6:   $��Right=�6c   6�   5"      � 5#   
   $��1��6   $��1��6   $��2��6,   �� 5$   
   $��Center=�6   $	��430��6.    �� Left$ 44��6;   
 �� Top$ 49��6L   $	��Top=�6\   $	��274��6t    �� CornerRadius$ 25��6�   ���	#6FFFFFFF._�5%      �  � RowDefinitions 5&      �� 5'      $	��406 �6$   .+�5+      �� -	   5         ��Name$ flashcardShareListBox��5+      |�. $��
20,73,20,0q�6J   $	��205��6^   $	��Top=�6k   �� 5,      -
   5         ��Name$ shareStackBtn��5,      $��Share Stack��60   $��20,340,20,0q�6b   $��30��6w   $	��Top=�6�   ���	#FFDECC8F$��MV Boli)�6�   �� 5-      -   5         ��Name$ shareStackTxt��5-       �� TextWrapping$
 Wrap=�61   $��20,307,20,81q�6E   $��18��6[   �� 5.      $��Enter user to share to:��6   $��20,278,101,0q�6:   $��MV Boli)�6P   $	��Top=�6e   �� 5/      $��Select a stack to share:��6   $��MV Boli)�6;   $	��Top=�6P   $��20,49,101,0q�6h   �� 50      $��49��6   $	��274��6%   $��Center=�61   $	��Top=�6N   $ 	25,25,0,0��6f   ���	#6FFFFFFF._�51      � .+�52      �� $��MV Boli)�6$   $��Center=�69   $	��154��6V   $��0,6,0,7q�6b   $��22(�6s    J� RenderTransformOrigin$ 0.5,0.5&�6�   z�53      V� 54   "   .~�55   &   ��  �� ScaleY$ 1Q�65   �� 56   &   �� 57   &   T� 58   &   59   #   �� 5:   :    � Language$ en-gb�6>   $�Share a stack��6O   6g   5;      5<      �� 5=      -   5        $��Share to the Public!��5=      $��20,383,20,-7q�68   $��30��6N   ���	#FFDECC8F$��MV Boli)�6q   6�   5>      5?      �� 5@   
   -   5         ��Name$ 	Backarrow��5@      $��/Images/backarrow.png��6#   $��Uniform=�6B   $��40��6T   $��40��6`   $	 404��6k   $	 -54��6|   $ 0.431,0.545&�6�   $��Center=�6�   $	��Top=�6�   z�5A      V� 5B      .~�5C      �� �� 5D      �� 5E      T� 5F       T� X$ 75Q�6)    T� Y$ 75Q�60   5G      5I      5J      5K      