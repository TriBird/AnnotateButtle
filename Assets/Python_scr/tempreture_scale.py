import pickle
import numpy as np
import pandas as pd
import matplotlib.pyplot as plt
from sklearn.neural_network import MLPClassifier
from sklearn.calibration import CalibratedClassifierCV
from scipy.optimize import minimize_scalar
from sklearn.metrics import brier_score_loss, log_loss
from sklearn.model_selection import train_test_split
from sklearn import svm

# refer.
# https://medium.com/@eskandar.sahel/applying-calibration-techniques-to-improve-probabilistic-predictions-in-machine-learning-models-c175c2e38ffc
# https://scikit-learn.org/stable/modules/generated/sklearn.neural_network.MLPClassifier.html
# http://taustation.com/sklearn-decision_function/

# dataset make for self-train
datasets = pd.read_json('Assets\\Resources\\datasets.json')
X = []
y = []
for index, row in datasets.iterrows():
	X.append(row[0]["embed"])
	y.append(row[0]["target"])

X_train, X_test, y_train, y_test = train_test_split(X, y, test_size=0.3, random_state=0, stratify=y)

clf = MLPClassifier(max_iter=10000, random_state=0)
clf.fit(X_train, y_train)

prob = clf.predict_proba(X_test)
