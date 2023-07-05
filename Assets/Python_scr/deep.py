import pickle
import numpy as np
import pandas as pd
import matplotlib.pyplot as plt
from sklearn.neural_network import MLPClassifier

# labeled infomation
labeld_index = pd.read_csv('Assets\\Resources\\labeled.csv', header=None).values.tolist()[0]

# dataset make for self-train
datasets = pd.read_json('Assets\\Resources\\datasets.json')
X_annotate = []
y_annotate = []
for index, row in datasets.iterrows():
	if index in labeld_index:
		X_annotate.append(row[0]["embed"])
		y_annotate.append(row[0]["target"])

# self-train
clf = MLPClassifier(max_iter=10000, random_state=0)
clf.fit(X_annotate, y_annotate)

# labeling
X = []
y = []
for index, row in datasets.iterrows():
	X.append(row[0]["embed"])
	y.append(clf.predict([row[0]["embed"]])[0])

# model load and train 
from sklearn.neural_network import MLPClassifier
clf = MLPClassifier(max_iter=10000, random_state=0)
clf.fit(X, y)

correct = 0
for index, row in datasets.iterrows():
	if clf.predict([row[0]["embed"]]) == row[0]["target"]:
		correct += 1

print(correct / len(datasets) * 100)
