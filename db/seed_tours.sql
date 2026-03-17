TRUNCATE TABLE "Tours" RESTART IDENTITY;

INSERT INTO "Tours" ("Name", "Price", "City", "CreatedAt")
VALUES
  ('Phú Quốc 3N2Đ - Sunset Town', 5990000, 'Phú Quốc', NOW() - INTERVAL '1 day'),
  ('Đà Nẵng 4N3Đ - Bà Nà Hills', 4890000, 'Đà Nẵng', NOW() - INTERVAL '2 days'),
  ('Nha Trang 3N2Đ - VinWonders', 4290000, 'Nha Trang', NOW() - INTERVAL '3 days'),
  ('Đà Lạt 3N2Đ - Săn mây Cầu Đất', 3390000, 'Đà Lạt', NOW() - INTERVAL '4 days'),
  ('Hà Nội 2N1Đ - Phố cổ & Ẩm thực', 1990000, 'Hà Nội', NOW() - INTERVAL '5 days'),
  ('Hạ Long 2N1Đ - Du thuyền vịnh', 3590000, 'Hạ Long', NOW() - INTERVAL '6 days'),
  ('Sa Pa 3N2Đ - Fansipan', 3990000, 'Sa Pa', NOW() - INTERVAL '7 days'),
  ('Huế 3N2Đ - Di sản cố đô', 3090000, 'Huế', NOW() - INTERVAL '8 days'),
  ('Quy Nhơn 3N2Đ - Kỳ Co Eo Gió', 3690000, 'Quy Nhơn', NOW() - INTERVAL '9 days'),
  ('Côn Đảo 3N2Đ - Hành trình tâm linh', 7990000, 'Côn Đảo', NOW() - INTERVAL '10 days'),
  ('Mũi Né 2N1Đ - Đồi cát bay', 2590000, 'Phan Thiết', NOW() - INTERVAL '11 days'),
  ('Cần Thơ 2N1Đ - Chợ nổi Cái Răng', 2190000, 'Cần Thơ', NOW() - INTERVAL '12 days');