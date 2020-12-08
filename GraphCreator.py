import matplotlib.pyplot as plt 
import numpy as np
import networkx as nx


#Данный код создает изображение графа
f = open('Graph_data.txt', 'r')
text=f.read()
j=0
a=[]

z=""
for i in text:
    if i=='\n':
         j+=1
    else:
          if i!=" ":
               z+=i
          else:
               a.append((j,int(z)))
               z=""
    
G=nx.MultiDiGraph(a)
nx.draw(G, with_labels=True, font_weight='bold',connectionstyle='arc3, rad = 0.2')
plt.savefig("GraphImg.png")

