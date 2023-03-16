# -*- coding: utf-8 -*-
"""
deserialize json
"""
import json
class DeserializeJson:
    # konstruktor
    def __init__(self, filename):
        print("let's deserialize something")
        tempdata = open(filename, encoding="utf8") #encoding utf-8 system kodowania znakow w formie 8-bitowej
        self.data = json.load(tempdata)

    # przykładowe statystyki
    def somestats(self):
        stats = {}
        for dep in self.data:
            w = str(dep['Województwo']).strip() # Nazwy niektórych województw zawierają spację na końcu -_-
            u = str(dep['typ_JST'])
            if w in stats:
                if u in stats[w]: stats[w][u] += 1
                else: stats[w].update({u: 1})
            else: stats.update({w: {u: 1}})
        for woj in stats:
            print("Wojewodztwo: " + str(woj))
            for urz in stats[woj]:
                print("Typ urzedu: " + str(urz) + ", liczba: " + str(stats[woj][urz]))