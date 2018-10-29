from flask import Flask, request
import pandas as pd
import numpy as np
import csv
from sklearn import tree
from sklearn import datasets
from sklearn import datasets, metrics
from sklearn.metrics import classification_report,confusion_matrix

app = Flask(__name__)

@app.route('/getTrainingData', methods=['POST','GET'])
def home():
    train = pd.read_csv("C:/Users/pgoel/Desktop/TrainingData.csv")
    print(train.as_matrix())
    feature_cols=['Distance','Duration']
    X=train.loc[:,feature_cols]
    print("training Data")
    print(X)
    y=train.Output
    print("training Output")
    print(y)
    clf=tree.DecisionTreeClassifier()
    clf.fit(X,y)
    test = pd.read_csv("C:/Users/pgoel/Desktop/TestingData.csv")
    print("testing data from file")
    print(test)
    print("hello")
    testing_feature_col=['Distance','Duration']
    out=train.append(test)
    print("type of out")
    out.to_csv(r"C:\Users\pgoel\Desktop\TrainingData.csv", index=False)
    print("output")
    print(out)
    testing_data=test.loc[:,testing_feature_col]
    print("testing_data read from test_data.csv file")
    print(testing_data)
    print("testing_output")
    predictions=clf.predict(testing_data)
    print(clf.predict(testing_data))
    print("testing Output to check for predictibility")
    print(test.Output)
    score = metrics.accuracy_score(test.Output,predictions)
    print ("Accuracy: %f" % score)
    print("GKUFGUIH")
    print(y.shape)
    return "Training Data Added To CSV file"




if __name__ == '__main__':
     app.run(debug=True)
