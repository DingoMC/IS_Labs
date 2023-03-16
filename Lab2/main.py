import yaml
from deserialize_json import DeserializeJson
from serialize_json import SerializeJson
from convert_json_to_yaml import ConvertJsonToYaml

tempconffile = open('Assets/basic_config.yaml', encoding="utf8")
confdata = yaml.load(tempconffile, Loader=yaml.FullLoader)

available_ops = {1: "Deserializacja pliku " +
                    confdata['paths']['source_folder'] +
                    confdata['paths']['json_source_file'],
                2: "Statystyki",
                3: "Serializacja do pliku " +
                   confdata['paths']['source_folder'] +
                   confdata['paths']['json_destination_file'],
                4: "Serializacja do pliku " +
                   confdata['paths']['source_folder'] +
                   confdata['paths']['yaml_destination_file'] + " z obiektu",
                5: "Serializacja do pliku " +
                   confdata['paths']['source_folder'] + confdata['paths']['yaml_destination_file'] +
                    " z pliku " + confdata['paths']['source_folder'] + confdata['paths']['json_source_file'],
                6: "Zakończ wczytywanie i wykonaj operacje"}
op_list = []
end = False

print("Dostępne operacje:")
for i in available_ops: print(str(i) + " -> " + available_ops[i])

while not end:
    x = 1
    try: x = int(input("Wybór: "))
    except ValueError: print("Error# To nie jest liczba!")
    else:
        if x < 1 or x > len(available_ops): print("Error# Liczba musi być z zakresu <1," + str(len(available_ops)) + ">")
        else:
            if x == 1 or x == 5: op_list.append(x)
            elif x == 2 or x == 3 or x == 4:
                if 1 in op_list: op_list.append(x)
                else: print("Error# Ta operacja wymaga wcześniejszego użycia operacji 1")
            else: end = True

print(f"Operacje do wykonania: {op_list}")

for i in op_list:
    print(f"Wykonywanie operacji {i}")
    if i == 1: newDeserializator = DeserializeJson(confdata['paths']['source_folder']+confdata['paths']['json_source_file'])
    elif i == 2: newDeserializator.somestats()
    elif i == 3: SerializeJson.run(newDeserializator, confdata['paths']['source_folder']+confdata['paths']['json_destination_file'])
    elif i == 4: ConvertJsonToYaml.run(newDeserializator, confdata['paths']['source_folder']+confdata['paths']['yaml_destination_file'])
    else: ConvertJsonToYaml.run(confdata['paths']['source_folder']+confdata['paths']['json_destination_file'],
                      confdata['paths']['source_folder']+confdata['paths']['yaml_destination_file'])
    print("OK")

print("Koniec")