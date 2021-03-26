namespace Heshed{
    class Zap{
        int idRecordBook;
        char[] lastname;
        char[] name;
        char[] patronymic;
        int idGroup;
        public int GetIdRecordBook(){
            return idRecordBook;
        }
        public char[] GetLastname(){
            return lastname;
        }
        public char[] GetName(){
            return name;
        }
        public char[] GetMiddlename(){
            return patronymic;
        }
        public int GetIdGroup(){
            return idGroup;
        }
        public Zap(int idRecordBook,char[] lastname,char[] name,char[] patronymic,int idGroup){
            this.idRecordBook = idRecordBook;
            this.lastname = lastname;
            this.name = name;
            this.patronymic = patronymic;
            this.idGroup = idGroup;
        }
        public Zap(Zap record){
            this.idRecordBook = record.idRecordBook;
            this.lastname = record.lastname;
            this.name = record.name;
            this.patronymic = record.patronymic;
            this.idGroup = record.idGroup;
        }
        public Zap()
        {
            idRecordBook=0;
            lastname=new char[0];
            name=new char[0];
            patronymic=new char[0];
            idGroup=0;
        }
    }
    class Block {
        Zap[] zapMass = new Zap[5];
        int Nextb;
        public int GetNextb {get => this.Nextb;}
        public void SetNextb(int Nextb){
            this.Nextb=Nextb;
            }
        public Zap GetZapMass(int i){
            return zapMass[i];
        }
        public void SetZapMass(int i,int idRecordBook,char[] lastname,char[] name,char[] patronymic,int idGroup){
            zapMass[i] = new Zap(idRecordBook,lastname,name,patronymic,idGroup);
        }
        public void SetZapMass(int i, Zap record)
        {
          zapMass[i] = new Zap(record.GetIdRecordBook(),record.GetLastname(),record.GetName(),record.GetMiddlename(),record.GetIdGroup());
        }



        public Block(Zap[] zapMass, int Nextb){
            Nextb=0;
            for(int i=0;i<5;i++)
            {
                this.zapMass[i] = zapMass[i];
            }
        }
        public Block(){
            Nextb=0;
            for(int i=0;i<5;i++)
            {
                zapMass[i] = new Zap();
            }
        }
    }
   /* class Block0{
        int size;

    }*/
}