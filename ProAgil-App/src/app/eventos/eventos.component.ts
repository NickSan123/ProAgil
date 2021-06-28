import { HttpClient } from '@angular/common/http';
import { Component, OnInit, resolveForwardRef } from '@angular/core';

@Component({
  selector: 'app-eventos',
  templateUrl: './eventos.component.html',
  styleUrls: ['./eventos.component.css']
})
export class EventosComponent implements OnInit {
 
  get filtroLista():string{
    return this._filtroLista;
  }

  set filtroLista (value: string){
    this._filtroLista = value;

    this.eventosFIltrados = this.filtroLista ? this.filtrarEventos(this.filtroLista):this.eventos;
  }

  eventosFIltrados: any = [];
  eventos: any = [];
  imagemLargura: number = 50;
  imagemMargem: number = 2;
  mostrarImg = false;
  _filtroLista!:string;

  filtrarEventos(filtrarPor: string): any {
    filtrarPor = filtrarPor.toLocaleLowerCase();

    return this.eventos.filter(
      (evento: { tema: string; }) => evento.tema.toLocaleLowerCase().indexOf(filtrarPor) !== -1
    );
  }
  

  constructor(private http: HttpClient) { }

  ngOnInit() 
  {
    this.getEventos();
  }
  alternarImagem(){
    this.mostrarImg = !this.mostrarImg;
  }
  getEventos()
  {
    //http://localhost:5000/values
    //https://servicodados.ibge.gov.br/api/v1/localidades/estados/{UF}/distritos
    this.http.get('http://localhost:5000/values').subscribe(response => {
      this.eventos = response;
      //console.log("OK");
    }, error => {
      console.log(error);
    });
  }
}
