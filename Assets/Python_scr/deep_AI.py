
import random
import csv
import numpy as np
import pandas as pd
import matplotlib.pyplot as plt
from sklearn.neural_network import MLPClassifier
from sklearn.calibration import CalibratedClassifierCV

# labeled infomation
labeld_index = []
labeld_index.append(random.randint(0, 9))
labeld_index.append(random.randint(10, 19))
labeld_index.append(random.randint(20, 29))
labeld_index.append(random.randint(30, 39))
labeld_index.append(random.randint(40, 49))

# dataset make for self-train
datasets = pd.read_json('Assets\\Resources\\datasets_auto.json')
X_annotate = []
y_annotate = []
for index, row in datasets.iterrows():
	if index in labeld_index:
		X_annotate.append(row[0]["embed"])
		y_annotate.append(row[0]["target"])

before_clf = MLPClassifier(hidden_layer_sizes=8, max_iter=10000, random_state=0)
before_clf.fit(X_annotate, y_annotate)

calib_clf = CalibratedClassifierCV(before_clf, method="sigmoid", cv="prefit")
calib_clf.fit(X_annotate, y_annotate)

# predict
problist = []
for index, row in datasets.iterrows():
    problist.append((index, calib_clf.predict_proba(np.array(row[0]["embed"]).reshape(1, -1))[0][row[0]["target"]]))

problist = sorted(problist, key=lambda x: x[1])

# add label
index = 0
while len(labeld_index) < 10:
    target = problist[index][0]
    page = target // 10 * 10

    if sum([1 if page <= x and x <= page+9 else 0 for x in labeld_index]) < 2:
        labeld_index.append(target)
	
    index += 1

labeld_index = sorted(labeld_index)

# datasets reconstruct
X_annotate = []
y_annotate = []
for index, row in datasets.iterrows():
	if index in labeld_index:
		X_annotate.append(row[0]["embed"])
		y_annotate.append(row[0]["target"])

# self-train
clf = MLPClassifier(hidden_layer_sizes=8, max_iter=10000, random_state=0)
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

f = open('C:\\Users\\shige\\HARPhone\\Assets\\Resources\\labeled_ai.csv', 'w')
writer = csv.writer(f)
writer.writerow(labeld_index)
f.close()