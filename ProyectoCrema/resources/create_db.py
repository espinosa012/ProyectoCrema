import csv
import sqlite3

# Conectar a la base de datos y crear una tabla
conn = sqlite3.connect('CosingDB.db')
c = conn.cursor()

with open('cosing_db.csv', encoding='utf-8') as archivo_csv:
    lector_csv = csv.reader(archivo_csv, delimiter=',')
    
    # Leer la primera fila para obtener los encabezados de columna
    encabezados = next(lector_csv)
    
    # Crear la tabla en la base de datos
    creating_table_sql_sentence = """CREATE TABLE IF NOT EXISTS INGREDIENTS (
        COSING_Ref_No integer PRIMARY KEY, 
        INCI_name text, 
        INN_name text, 
        Ph_Eur_Name text, 
        CAS_No text, 
        EC_No text, 
        Chem_IUPAC_Name_Description text, 
        Restriction text, 
        Function text, 
        Update_Date text
    )"""
    c.execute(creating_table_sql_sentence)

    # corregimos el nombre de los encabezados
    encabezados = ["COSING_Ref_No", "INCI_name", "INN_name", "Ph_Eur_Name", "CAS_No", "EC_No", "Chem_IUPAC_Name_Description", "Restriction", "Function", "Update_Date"]
    error = 0
    # Construir la sentencia SQL para insertar los datos
    for fila in lector_csv:
        campos = [f.strip() for f in fila]
        
        # obtenemos la clave primaria
        primary_key = int(campos.pop(0))
        campos = ["'{}'".format(c.strip()) for c in campos]
        sentencia_sql = 'INSERT INTO INGREDIENTS ({}) VALUES ({}, {})'.format(', '.join(encabezados), primary_key, ', '.join(campos))
        # input(sentencia_sql)

        # Insertar la fila en la base de datos
        try:
            c.execute(sentencia_sql)
        except Exception as e:
            error += 1
            print(e)
            print("-------------------------------------")

# # Guardar los cambios y cerrar la conexión
conn.commit()
conn.close()
print(error)