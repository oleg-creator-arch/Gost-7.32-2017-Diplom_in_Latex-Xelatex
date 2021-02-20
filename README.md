Выпускная квалификационная работа (ВКР) бакалавра в LaTeX, оформленная в соответствии с нормоконтролем Южного Федерального Университета(ЮФУ) в 2021 г.

## Особенности

## Примечания
Для установки LaTeX
```
sudo apt install texlive-base texlive-latex-extra texlive-xetex texlive-lang-cyrillic latexmk texlive-fonts-extra texlive-science texlive-latex-recommended
```
Для установки шрифтов Times New Roman, XITS Math, PT Sans, PT Mono 

```
sudo apt install ttf-mscorefonts-installer
sudo wget -O /usr/share/fonts/xits-math.otf https://github.com/khaledhosny/xits-math/raw/master/XITSMath-Regular.otf
sudo wget https://ftp.tw.freebsd.org/distfiles/xorg/font/{PTSansOFL,PTMonoOFL}.zip
sudo unzip -o PTSansOFL.zip -d /usr/share/fonts/ && sudo unzip -o PTMonoOFL.zip -d /usr/share/fonts/
sudo rm -f {PTSansOFL,PTMonoOFL}.zip && sudo fc-cache -f -v
```
