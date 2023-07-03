import pandas as pd
import pickle

datasets = None
with open('Python_scr\\datasets_en_embeded.pkl', 'rb') as f:
    datasets = pickle.load(f)

# for index, row in datasets.iterrows():
#     print(row[2])

path = 'datasets.json'
datasets.to_json(path)