using System;
using System.IO;
namespace BDlab1{
    partial class OurBlock{
        //Переделан без return
        public int Search(int idRecordBook,string filename)
        {
            using (BinaryReader reader = new BinaryReader(File.Open(filename, FileMode.Open)))
            {
                byte[] blockBinary = new byte[440];
                int numBlock = ReadNullBlockInt(reader);
                int numZapFound;
                for(int i=0;i<numBlock;i++)
                {
                    reader.Read(blockBinary, 0, 440);
                    ByteArrToBlock(blockBinary);
                    if((numZapFound=FindStudent(idRecordBook))!=-1)
                    {
                        reader.Close();
                        return (numZapFound)*88;
                    }
                }
                reader.Close();
                return -1;
            }
        }

        //Не переделан
        public int Edit(string filename,int oldidRecordBook,int idRecordBook,string lastname,string name, string patronymic,int idGroup)
        {
            int numZap=Search(oldidRecordBook,filename);
            if(numZap==-1)
            {
                return -1;
            }
            numZap+=4;
            using (BinaryWriter writer=new BinaryWriter(File.Open(filename, FileMode.Open)))
            {
                writer.Seek(numZap,SeekOrigin.Begin);
                writer.Write(idRecordBook);
                writer.Write(InChar(lastname,30));
                writer.Write(InChar(name,20));
                writer.Write(InChar(patronymic,30));
                writer.Write(idGroup); 
                writer.Close();              
            }
            Search(idRecordBook,filename);
            return 0;
        }

        //Не переделан
        public void Remove(int idRecordBook,string filename)
        {
            // Удаление последнего блока
                /*FileStream fileStream = new FileStream(filename, FileMode.Open);
                fileStream.SetLength(440);*/
            int sizeZap;
            using (BinaryReader reader = new BinaryReader(File.Open(filename, FileMode.Open)))
            {
                sizeZap = reader.ReadInt32();
                if(sizeZap==1)
                {
                    Console.WriteLine("Мы ничего не можем удалить, так как БД пуста");
                    return;
                }  
                for(int i=0;i<sizeZap-1;i++)
                {
                    block.SetZapMass(i%5,reader.ReadInt32(),ByteChar(reader,30),ByteChar(reader,20),ByteChar(reader,30),reader.ReadInt32());
                }
                reader.Close();
            }
            if(Edit(filename,idRecordBook,block.GetZapMass((sizeZap-2)%5).GetIdRecordBook(), InString(block.GetZapMass((sizeZap-2)%5).GetLastname(),30),InString(block.GetZapMass((sizeZap-2)%5).GetName(),20),InString(block.GetZapMass((sizeZap-2)%5).GetMiddlename(),30),block.GetZapMass((sizeZap-2)%5).GetIdGroup())==-1)
            {
                return;
            }
            using (BinaryWriter writer=new BinaryWriter(File.Open(filename, FileMode.Open)))
            {
                writer.Write(sizeZap-1);
                writer.Close();
            }
        }

        //Переделан
        public void AddOnEnd(string filename, int idRecordBook,string lastname,string name,string patronymic,int idGroup)
        {
            if(idRecordBook==0){
                Console.WriteLine("Нельзя присвоить этот номер зачётки");
                return;
            }
            if(Search(idRecordBook,filename)!=-1){
                Console.WriteLine("Номер зачётки занят");
                return;
            }
            int numBlock = ReadNullBlockInt(filename);
            if(Search(0,filename)!=-1){
                byte[] blockBinary = new byte[440];
                using (var reader = File.Open(filename, FileMode.Open))
                {
                    reader.Seek((numBlock-1)*440+4, SeekOrigin.Begin);
                    reader.Read(blockBinary, 0, 440);
                }
                ByteArrToBlock(blockBinary);
                AddZapOnEnd(idRecordBook,lastname,name,patronymic,idGroup);
                blockBinary=Combine();
                using (BinaryWriter writer=new BinaryWriter(File.Open(filename, FileMode.Open)))
                {
                    writer.Seek((numBlock-1)*440+4,SeekOrigin.Begin);
                    writer.Write(blockBinary);
                } 
            }
            else{
                using (BinaryWriter writer=new BinaryWriter(File.Open(filename, FileMode.Open)))
                {
                    writer.Write(numBlock+1);
                    writer.Seek(numBlock*440+4,SeekOrigin.Begin);
                    block.SetZapMass(0,idRecordBook,InChar(lastname,30),InChar(name,20),InChar(patronymic,30),idGroup);
                    byte[] blockBinary=Combine(block.GetZapMass(0));
                    Array.Resize(ref blockBinary,440);
                    writer.Write(blockBinary);
                }
            }
        }
    }
}