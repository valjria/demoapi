namespace demoapi.DTO
{
      
      public class PaginationDto<T>
    {
        public IEnumerable<T> Items { get; set; } = new List<T>(); // Generic türde sayfa verileri
        public int CurrentPage { get; set; } // Şu anki sayfa
        public int PageSize { get; set; } // Sayfa başına kayıt sayısı
        public int TotalCount { get; set; } // Toplam kayıt sayısı
        public int TotalPages => (int)Math.Ceiling((double)TotalCount / PageSize); // Toplam sayfa sayısı
    }

}