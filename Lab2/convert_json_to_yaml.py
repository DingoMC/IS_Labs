# -*- coding: utf-8 -*-
"""
json to yaml converter
"""
import yaml
import json

class ConvertJsonToYaml:

    @staticmethod
    def run(data_in, destinationfilelocaiton : str):
        if isinstance(data_in, str):
            print('Konwersja JSON -> YAML')
            tempdata = json.load(open(data_in, encoding="utf8"))
            with open(destinationfilelocaiton, 'w', encoding='utf-8') as f:
                yaml.dump(tempdata, f, allow_unicode=True)
            print('Konwersja zakonczona')
        else:
            print("let's convert something")
            with open(destinationfilelocaiton, 'w', encoding='utf-8') as f:
                yaml.dump(data_in, f, allow_unicode=True)
                print("it is done")

