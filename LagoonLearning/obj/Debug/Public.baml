   M S B A M L   `   `   `  ����I  ELagoonLearning, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null/clr-namespace:LagoonLearningLagoonLearning  5   	       LagoonLearning.Public   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   .localclr-namespace:LagoonLearning     5   	     � Title$  Public��5   	   $	��539��6   $	��363��6%    � 
ResizeMode$ NoResize=�61    � WindowStyle$
 None=�6G    � SizeToContent$ Manual=�6Z    � WindowStartupLocation$ CenterScreen=�6q   .��5	      �� ��5
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
 PublicB��5      $��Publications��6,   {�.  6V   m� 5      -   5        $
 Share��5      $��Share��6*   6S   5       m� 5!      -   5        $
��Exit��5!      $��MV Boli)�6%   $
��Bold"�6:   $��Right=�6c   6�   5"      � 5#   
   $��1��6   $��1��6   $��2��6,   �� 5$   
   $��Center=�6   $	��406��6.    �� Left$ 44��6;   
 �� Top$ 64��6L   $	��Top=�6\   $	��274��6t    �� CornerRadius$ 25��6�   ���	#6FFFFFFF._�5%      �  � RowDefinitions 5&      �� 5'      $	��406 �6$   .+�5*      �� -	   5        $��flashcardPublicListView��5*      |�. $��
20,78,20,0q�6K   $	��253��6_   $	��Top=�6l   $��Disabled=�6�   $
��Auto=�6�   $��MV Boli)�6�   $��Normal"�6�    �� View 5+      �� 5,      .P�5-      �� $��Stack��6-    �� Width$	 156��6c    �� DisplayMemberBinding 6<   �� *Title+6c   �� 5.      $��Score��6-   $ 78��6d    6<   �� *Author+6d   5/      51      �� 52      -
   5         ��Name$ importStackBtn��52      $��Import Stack��61   $��20,354,20,0q�6e   $��30��6z   $	��Top=�6�   ���	#FFDECC8F$��MV Boli)�6�   �� 53      $��Select a stack to import:��6   $��MV Boli)�6<   $	��Top=�6Q   $��20,49,101,0q�6i   �� 54      $��49��6   $	��274��6%   $��Center=�61   $	��Top=�6N   $ 	25,25,0,0��6f   ���	#6FFFFFFF._�55      � .+�56      �� $��MV Boli)�6$   $��Center=�69   $	��174��6V   $��0,6,0,7q�6b   $��22(�6s    J� RenderTransformOrigin$ 0.5,0.5&�6�   z�57      V� 58   "   .~�59   &   ��  �� ScaleY$ 1Q�65   �� 5:   &   �� 5;   &   T� 5<   &   5=   #   �� 5>   :    � Language$ en-gb�6>   $�Import a Stack!��6O   6i   5?      5@      5A      5B      �� 5C   
   -   5         ��Name$ 	Backarrow��5C      $��/Images/backarrow.png��6#   $��Uniform=�6B   $��40��6T   $��40��6`   $	 404��6k   $	 -54��6|   $ 0.431,0.545&�6�   $��Center=�6�   $	��Top=�6�   z�5D      V� 5E      .~�5F      �� �� 5G      �� 5H      T� 5I       T� X$ 75Q�6)    T� Y$ 75Q�60   5J      5L      5M      5N      