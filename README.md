# ONE DAY AT PEAK

***

Bu etkinlikteki amacımız bir gün içinde Nokia 3310 da bulunan klasik yılan oyununun 3D bir versiyonu geliştirmek. Bu oyunu geliştirirken bizim de yaşadığımız geliştirme süreçlerinin bir kısmını yaşayacak ve bizim oyun projelerimizdeki mühendislerle birlikte çalışacaksınız. Etkinliğin sonunda da yapılan projelerin code reviewları olacak.

Projeyi git üzerinden sizlerle paylaşacağız. Projeyi geliştirirken tamamladığınız adımları commit'leyip repositorylerinize push'lamanızı bekliyoruz. 

Projeyi Unity Engine üzerinde geliştireceğiz. Etkinlik süresince **X4-PRBP-5EHY-9YEF-T4F9-ECF5** lisansını aktive edip kullanmanızı rica ediyoruz.



***

### Proje Altyapısı

Sahneyi açtığınızda büyük bir oyun alanı göreceksiniz. Biz bu alana dökümanda ve oyun içerisinde **GameGrid** diyoruz. Bu Grid, sayısı editörden ayarlanabilen, küçük karelerden oluşmaktadır. Bu küçük karelere ise **GridCell** diyoruz.

Karelerin üzerinde oyun boyunca kullanacağımız **Snake**’imizi göreceksiniz. Snake’imiz ise head, body ve tail parçalarından oluşmakta. Bu parçalardan her birine biz **SnakeBodyPart** diyoruz. Bu bahsettiklerimiz oyun içerisinde hazır ve çalışır şekilde bulunuyor.

Yılanın hareketini saniyede beş defa güncelliyoruz, bu güncellemelerin her birine **Tick** ismini veriyoruz.

Size vereceğimiz görevleri mevcut sistemin üzerine geliştirme ve değiştirme yaparak tamamlamanızı bekliyoruz.

***

### Görevler
Görevleri yaparken dikkat etmeniz gereken bazı noktalar var:

- Bu projenin uzun soluklu gerçek bir oyun olduğunu düşünerek geliştirme yapmanız gerekiyor.
- Yazdığınız kodun temiz ve başkaları tarafından rahatça anlaşılır olması gerekiyor.
- Görevleri takım olarak, beraber tamamlamanız gerekiyor.

Görevleri yaparken ihtiyacınız olan tüm görselleri biz önceden hazırladık ve kod ile birlikte projenin içine ekleyerek sizin kullanımınıza hazır hale getirdik. İhtiyacınız olan bütün görseller projede mevcuttur.



***

## Görev 1 - Yılanın Hareketi

İlk görevimizde, oyunun ana mekaniklerinden olan yılanın ilerleme hareketini oyuna eklemenizi bekliyoruz.
Yılanın kafası istenilen yöne doğru hareket ederken, vücut parçaları sırayla birbirlerini takip ediyor olmalı.

### 1. Yılanın parçalarının hazırlanması

Oyun, kafası **GameGrid**'in merkezinde olan 4 birim uzunluğunda bir yılan ile başlamalı.

### 2. Yılanın hareketinin eklenmesi

Klasik yılan oyununda olduğu gibi, yılanın kafadan sonra gelen her bir kısmı kendinden önceki vücut parçasının hareketini kopyalıyor olmalı.

### 3. Yılanın yönünün kullanıcı tarafından değiştirilebilmesi



***

## Görev 2 - Yılanın Büyümesi

İkinci görevimizide yılanın elmaları yiyerek büyümesini sağlamanızı bekliyoruz.

### 1. Elmanın oluşması
Elma boş bir GridCell üzerinde rasgele olarak oluşmalı. Yılan'ın kafası, elmanın bulunduğu GridCell'e girince elma yok olmalı ve boş bir GridCell'de tekrar oluşmalı. 

### 2. Yılanın elmayı yediğinde boyunun uzaması
Yılan elmayı yediğinde uzunluğu bir birim artmalı.



---

## Görev 3 - Yılanın kendi vücuduna veya bir sandığa çarptığında ölmesi

Üçüncü görevimizde yılanın kendisine veya bir sandığa çarptığında ölmesini ve oyunun durmasını bekliyoruz.

### 1. Yılanın kendisine çarpınca ölmesi
Yılan kendisine çarptığı zaman oyun durmalı ve 'Başarısız Oldun' ekranı gösterilmeli.

### 2. Sandığın eklenmesi
GridCell üzerine sandık objesi ekleyebilmeliyiz. Yılan sandığa çarptığı zaman oyun durmalı ve kullanıcı seviyeyi kayıp etmeli.



---


## Görev 4 - Kafanın ve kuyruğun görsellerinin değiştirilmesi

Bu görevimizde yılanın kafa ve kuyruk görsellerinin değiştirilmesini bekliyoruz.

### 1. Görsellerin oluşturulması
Eski görseller yerine oyun içerisinde bulunan kafa, vücut ve kuyruk görselleri eklenmeli.

### 2. Görsellerin rotasyonlarının düzenlenmesi
Yılanın kafa ve kuyruk görselleri yılanın gittiği yöne göre düzgün gözükmeli.



---

## Görev 5 - Seviyelerin veriye göre oluşturulması

Bu adımda seviyelerin elimizdeki veriye göre oluşturulmasını bekliyoruz. 



---

## Görev 6 - Hedefler ve Seviyelerin oyuna eklenmesi

Bu görevimizde oyunumuza hedeflerin eklenmesini ve bu hedeflere ulaşıldığında bir sonraki seviyeye geçilmesini bekliyoruz.

### 1. Hedeflerin eklenmesi
Oyunda her seviyenin bir hedefi var. Yılan GridCell üzerindeki objeleri yiyerek bu hedefleri tamamlayabilmeli.

### 2. Hedeflerin ana ekranda gösterilmesi
Kullanıcı hedefleri arayüzde görebilmeli. Ayrıca hedeflerin sayısı dinamik olarak güncellenebilmeli.

### 3. Hedefler tamamlanınca bir sonraki seviyeye geçilmesi
Bütün hedefler tamamlandığında bir sonraki seviyenin yüklenmesini bekliyoruz. 



---

## Görev 7 - Portal eklenmesi

Bu görevimizde yılanı GameGrid'in ucuna ışınlayacak portal nesnesinin eklemenizi bekliyoruz.

Yılan portala girdiğinde, girdiği yönün tersindeki en uzak uygun kareden çıkış yapıyor olmalı. Çıkış yaptığı kare üzerinde sandık veya portal bulunmamalı. Çıkış yaptığı karede yıllanın bir vücut parçası varsa ölmeli, elma varsa yemeli. 



---

## Oyunun Geliştirilmesi

Tebrikler! Bu noktaya ulaştığınıza göre görevleri başarılı bir şekilde tamamlamışsınız demektir. Şimdi oyununuzun hissiyatını güçlendirecek olan geliştirmeler tasarlayıp, onları ekleyebilirsiniz.


---
