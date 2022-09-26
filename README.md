# Encryption
Реализация некоторых видов шифрования
# Описание классов
- Интерфейс [ICryptographer](https://github.com/erikrause/Encryption/blob/master/Classes/ICryptographer.cs) используется классами, реализующими шифрование.
- Класс [BitCryptographer](https://github.com/erikrause/Encryption/blob/master/Classes/BitCryptographer.cs) реализует симметричное шифрование смещением битов.
- Класс [KeyCryptographer](https://github.com/erikrause/Encryption/blob/master/Classes/KeyCryptographer.cs) реализует симметричное шифрование Цезаря (сдвиг).
- Класс [GOSTCryptographer](https://github.com/erikrause/Encryption/blob/master/Classes/GOSTCryptographer.cs) реализует симметричное шифрование ГОСТ 28147-89 в режиме простой замены.
- Класс [RSACryptographer](https://github.com/erikrause/Encryption/blob/master/Classes/RSACryptographer.cs) реализует ассиметричное шифрование RSA.
# Примеры
<p align="center"><img src="https://github.com/erikrause/Encryption/blob/master/examples/Bit%20shift.png" width="50%"></p>
<p align="center"><img src="https://github.com/erikrause/Encryption/blob/master/examples/Key%20shift.png" width="50%"></p>
<p align="center"><img src="https://github.com/erikrause/Encryption/blob/master/examples/GOST.png" width="50%"></p>
<p align="center"><img src="https://github.com/erikrause/Encryption/blob/master/examples/RSA.png" width="50%"></p>
