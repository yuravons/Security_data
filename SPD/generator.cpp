#include "generator.h"

Generator::Generator()
{
    m = pow(2, 12) - 1;
    a = pow(4, 5);
    c = 2;
    X0 = 8;
}

void Generator::generateValues()
{
    x = new uint[nSize];
    x[0] = X0;
    for(uint n = 0; n < nSize - 1; ++n )
        x[n+1] = (a*x[n] + c) % m;
}

void Generator::setSize(uint nValue)
{
    nSize = nValue;
}

uint Generator::getSize()
{
    return nSize;
}

uint * Generator::getArray()
{
    return x;
}
