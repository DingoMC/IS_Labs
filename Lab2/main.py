import sys

import yaml
from deserialize_json import DeserializeJson
from serialize_json import SerializeJson
from convert_json_to_yaml import ConvertJsonToYaml

tempconffile = open('Assets/basic_config.yaml', encoding="utf8")
confdata = yaml.load(tempconffile, Loader=yaml.FullLoader)
op_list = confdata['operations']
found_1 = False

print("Dostępne operacje:")
for i in range(0, len(confdata['available_ops'])): print(str(i+1) + " -> " + confdata['available_ops'][i])

print("Sprawdzanie konfiguracji...")
for i in op_list:
    x = 1
    try: x = int(i)
    except ValueError:
        print("Error# Błąd konfiguracji! Nieprawidłowa wartość")
        sys.exit(0)
    else:
        if x < 1 or x > len(confdata['available_ops']):
            print("Error# Błąd konfiguracji! Nieprawidłowa wartość")
            sys.exit(0)
        if x == 1: found_1 = True
        if x == 2 or x == 3 or x == 4:
            if not found_1:
                print("Error# Błąd konfiguracji! Ta operacja wymaga wcześniejszego użycia operacji 1")
                sys.exit(0)

print("Konfiguracja poprawna")

print(f"Operacje do wykonania:")

for i in op_list:
    print(confdata['available_ops'][i-1], end=", ")
print()

for i in op_list:
    print(f"Wykonywanie operacji {confdata['available_ops'][i-1]}")
    if i == 1: newDeserializator = DeserializeJson(confdata['paths']['source_folder']+confdata['paths']['json_source_file'])
    elif i == 2: newDeserializator.somestats()
    elif i == 3: SerializeJson.run(newDeserializator, confdata['paths']['source_folder']+confdata['paths']['json_destination_file'])
    elif i == 4: ConvertJsonToYaml.run(newDeserializator, confdata['paths']['source_folder']+confdata['paths']['yaml_destination_file'])
    else: ConvertJsonToYaml.run(confdata['paths']['source_folder']+confdata['paths']['json_destination_file'],
                      confdata['paths']['source_folder']+confdata['paths']['yaml_destination_file'])
    print("OK")

print("Koniec")
