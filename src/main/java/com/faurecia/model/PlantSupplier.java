package com.faurecia.model;

import javax.persistence.Entity;
import javax.persistence.Table;

import org.apache.commons.lang.builder.ToStringBuilder;
import org.apache.commons.lang.builder.ToStringStyle;

@Entity
public class PlantSupplier extends BaseObject {

	/**
	 * 
	 */
	private static final long serialVersionUID = -6616983854718294391L;

	private int id;

	public int getId() {
		return id;
	}

	public void setId(int id) {
		this.id = id;
	}

	/**
     * {@inheritDoc}
     */
    public boolean equals(Object o) {
        if (this == o) {
            return true;
        }
        if (!(o instanceof PlantSupplier)) {
            return false;
        }

        final PlantSupplier plantSupplier = (PlantSupplier) o;

        return id == plantSupplier.id;

    }
    
	/**
     * {@inheritDoc}
     */
    public int hashCode() {
        return id;
    }

    /**
     * {@inheritDoc}
     */
    public String toString() {
        return new ToStringBuilder(this, ToStringStyle.SIMPLE_STYLE)
                .append(this.id)
                .toString();
    }

    public int compareTo(Object o) {
        return (equals(o) ? 0 : -1);
    }
}
