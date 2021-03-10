import numpy as np

str = ('АБВГДЕЁЖЗИЙКЛМНОПРСТУФХЧШЦЩЪЫЬЭЮЯабвгдеёжзийклмнопрстуфхчшцщъыьэюя .:!?,')

n = 3

key = np.array([[1, 3, 2], [1, 3, 5], [3, 2, 1]])
while (np.linalg.det(key)==0):
    print('детерминант матрицы ключа равен 0, измените матрицу ключа')
    input()


print('Зашифровать строку введите 1')
print('Расшифровать строку введите 2')
print('Закончить 0')

run = input()
while (run != '0'):
    if (run == '1'):
        print('Введите строку')
        inp = input()
        if(len(inp) % 3 == 2):
            inp +=' '
        if (len(inp) % 3 == 1):
            inp += '  '
        codeALf = []
        code = []

        sucsessFlag = True
        for m in range(len(inp)):
            flag = True
            for l in str:
                if l == inp[m]:
                    codeALf.append(str.index(l)+1) # +1
                    flag = False
            if flag:
                sucsessFlag = False
                print('В введёной строке присутствует недопустимый символ')
                break

        if sucsessFlag:
            codeALfnp = np.reshape(np.array(codeALf), (len(inp) // 3, 3))

            for i in range(len(inp) // 3):
                code.append(key.dot(codeALfnp[i].transpose()))
            print('Зашифрованная строка:')
            print(np.reshape(np.array(code), (1,len(inp))))

    if run == '2':
        print('Введите зашифрованную строку')
        code = input()
        codeSPL = code.split(' ')
        codeNP = np.reshape(np.array(codeSPL), (len(codeSPL)//3, 3))

        inpCod = []
        keyOb = np.linalg.inv(key)
        for i in range(len(codeSPL) // 3):
            inpCod.append(keyOb.dot(codeNP[i].transpose().astype(float)))
        inpCodVek = (np.reshape(inpCod, (1, len(codeSPL)))+0.5).astype(int)
        print('Исходный текст:')
        for i in inpCodVek[0]:
            print(str[i-1], end = "")
    print()


    print('Зашифровать строку введите 1')
    print('Расшифровать строку введите 2')
    print('Закончить 0')
    run = input()



