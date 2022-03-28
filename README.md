# ex-strong-ids
### Problema:
É Comum utilizamos tipos primitivos para representar os IDs das entidades

```cs Example
public class ClasseComIdInteiro
{
  public int Id { get; set; }
}

public class ClasseComIdGuid
{
  public Guid Id { get; set; }
}
```

Dessa forma as classes podem ter ids de tipos intercambiáveis

```cs Example
public void Funcao123(long idDaClasseA, long idDaClasseB)
{
  ...
}

Funcao123(classeB.Id, classeA.Id); //parâmetros invertidos!!!
```

### Proposta de Solução:
Utilizar [records](https://docs.microsoft.com/en-us/dotnet/csharp/whats-new/tutorials/records) para definir os tipos dos ids

```cs Example
public record Entidade123Id(int Value);

public class Entidade123
{
  public Entidade123Id Id { get; set; }
}
```

Agora cada ID é fortemente tipado

```cs Example
public void FuncaoABC(EntidadeXId idDaClasseX, EntidadeYId idDaClasseY)
{
  ...
}

Funcao123(classeY.Id, classeX.Id); //não compila!!!
```
