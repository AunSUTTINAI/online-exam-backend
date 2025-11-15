# Online Exam System — Backend

โปรเจคนี้เป็นส่วนของ Backend API
พัฒนาด้วย C# .NET 10 ใช้รูปแบบการจัดการ API => Endpoint  
โดยโปรเจคออกแบบมาให้สามารถใช้งานได้ทันที ไม่ต้องตั้งค่า Database 
เพราะระบบใช้ ไฟล์ JSON  และไฟล์ Log.txt แทน Database

เทคโนโลยีที่ใช้

- C# .NET Core 10
- Endpoint Routing
- Dependency Injection (Scoped Services)

Data Storage
- ใช้ไฟล์แทน Database:
  - online-exam/Masters/master_questions.json เก็บชุดคำถาม-คำตอบทั้งหมด
  - online-exam/ExamHistory/{fullName}.txt เก็บประวัติการสอบของผู้เข้าสอบแต่ละคน

เหตุผลที่ไม่ใช้ Database
ระบบถูกออกแบบให้:
- ใช้งานได้ทันทีหลัง Clone Repo 
- ไม่ต้อง config database ยาก
- ใช้ได้ทุกเครื่อง
