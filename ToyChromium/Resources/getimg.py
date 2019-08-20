import urllib.request
import ssl
from bs4 import BeautifulSoup

#这里的路径要根据实际情况改成绝对路径，其它无需设置
filePath = 'd:\\ToyChromium\\Template\\fillimg\\a.jpg'

ssl._create_default_https_context = ssl._create_unverified_context

def run():
    url = 'https://www.flickr.com/photos/esa_marswebcam/'
    res = urllib.request.urlopen(url)
    html = res.read()
    soup = BeautifulSoup(html)
    newImgUrl = soup.find(attrs={'property': 'og:image'})['content']
    urllib.request.urlretrieve(newImgUrl, filePath)


if __name__ == '__main__':
    run()
