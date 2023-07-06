import pandas as pd
import pickle

datasets = None
with open('Assets\\Resources\\datasets_en_embeded.pkl', 'rb') as f:
    datasets = pickle.load(f)

datasets = datasets.sample(frac=1).sort_values('target')

for index, row in datasets.iterrows():
    print(row[1], row[0])

path = 'Assets\\Resources\\datasets.json'
datasets.to_json(path, orient="records")