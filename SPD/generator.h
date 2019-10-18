#ifndef GENERATOR_H
#define GENERATOR_H

#include <QObject>
#include <cmath>
#include <QString>

class Generator
{
public:
    Generator();
    void generateValues();
    void setSize(uint nValue);
    uint getSize();
    uint * getArray();
private:
    uint nSize;
    uint *x;
    uint m, a , c , X0;
};

#endif // GENERATOR_H
