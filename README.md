Örnek Proje: Eğitim Planlama Sistemi
Projenin Detayları
Amaç: Havacılık sektöründe kabin ve kokpit personeli için eğitim süreçlerini yönetmek.
Temel Özellikler: 
Ders Yönetimi: Dersler ve konuların tanımlanması.
Öğrenci Yönetimi: Kabin ve kokpit personelinin kayıtları ve rolleri.
Not Sistemi: Öğrencilerin aldıkları derslere göre notların tutulması.
Raporlama: Öğrencilerin başarı durumları ve aldıkları derslerin raporlanması.
Veritabanı Yapısı
Tablolar: 
Courses (Dersler): CourseId, CourseName, Description.
Topics (Konular): TopicId, TopicName, CourseId.
Students: StudentId, Name, Role (Kabin/Kokpit).
Grades: GradeId, StudentId, CourseId, Grade.
API İşlemleri
Dersler: 
Ders ekleme, listeleme, güncelleme, silme.
GET /api/courses, POST /api/courses.
Konular: 
Bir derse bağlı konuları ekleme ve listeleme.
GET /api/courses/{id}/topics, POST /api/topics.
Öğrenciler: 
Öğrenci ekleme ve listeleme.
GET /api/students, POST /api/students.
Notlar: 
Not ekleme ve listeleme.
GET /api/grades, POST /api/grades.
Görev Dağılımı:
Veritabanı Tasarımı: İlişkiler ve tabloları oluştur.
Backend API: RESTful API’yi geliştir ve CRUD işlemleri için endpoint’ler oluştur.
Test: Postman ile API’yi test et.
Raporlama: Öğrenci başarı durumları için bir endpoint oluştur (örn: GET /api/reports).
